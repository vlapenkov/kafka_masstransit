1. ������� ����� ������� record VideoCreatedEvent ��� �������� ������ ������ � kafka
������ �����:
docker exec -t broker1  kafka-topics.sh   --bootstrap-server :9092     --list
VideoCreatedEvent
VideoDeletedEvent

�� ������� ������� ���������� ��������� VideoCreatedEvent � �����, �� ������� �����