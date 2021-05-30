using KafkaMasstransitWebApp1.Events;
using KafkaMasstransitWebApp1.Handlers;
using MassTransit;
using MassTransit.KafkaIntegration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Net;
using System.Reflection;

namespace KafkaMasstransitWebApp1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.IncludeXmlComments(@"KafkaMasstransitWebApp1.xml");
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApplication1: Send create event", Version = "v1" });
            });


            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) => cfg.ConfigureEndpoints(context));

                x.AddRider(rider =>
                {
                    rider.AddConsumer<VideoDeletedEventConsumer>();
                    rider.AddProducer<VideoCreatedEvent>(nameof(VideoCreatedEvent));

                    rider.UsingKafka((context, k) =>
                    {
                        k.Host("localhost:9092");

                        k.TopicEndpoint<VideoDeletedEvent>(nameof(VideoDeletedEvent), GetUniqueName(nameof(VideoDeletedEvent)), e =>
                        {
                            // e.AutoOffsetReset = AutoOffsetReset.Latest;
                            //e.ConcurrencyLimit = 3;
                            e.CheckpointInterval = TimeSpan.FromSeconds(10);
                            e.ConfigureConsumer<VideoDeletedEventConsumer>(context);

                            e.CreateIfMissing(t =>
                            {
                                //t.NumPartitions = 2; //number of partitions
                                //t.ReplicationFactor = 1; //number of replicas
                            });
                        });
                    });
                });
            });

            services.AddMassTransitHostedService(true);
        }

        private static string GetUniqueName(string eventName)
        {
            string hostName = Dns.GetHostName();
            string callingAssembly = Assembly.GetCallingAssembly().GetName().Name;
            return $"{hostName}.{callingAssembly}.{eventName}";
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApplication1 v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            var lifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();

            lifetime.ApplicationStopping.Register(() =>
            {
                Console.WriteLine("ApplicationStopping");
            });
        }
    }
}
