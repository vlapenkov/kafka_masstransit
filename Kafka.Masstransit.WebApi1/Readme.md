1. Добавил более сложный record VideoCreatedEvent для передачи внутри топика в kafka
Топики такие:
docker exec -t broker1  kafka-topics.sh   --bootstrap-server :9092     --list
VideoCreatedEvent
VideoDeletedEvent

Из первого проекта отправляем сообщение VideoCreatedEvent в топик, из второго ловим