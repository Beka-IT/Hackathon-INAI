INSERT INTO Operations (Title,DurationInMinutes)
VALUES ('�������', 5),('������', 5),('������',20),('�������� �����',30);

INSERT INTO Branches (Title,Address,Longitude,Latitude)
VALUES ('�������� ����','����������, 80/1',42.87713,74.57412),('����� �������� ���������','����� ������, 338',42.88036, 74.61264),('��������� � 033-0-01','���������� 185',42.87210,74.58400),('����������� ������','��� 48',42.87543, 74.64734);


INSERT INTO Departments (Title,BranchId)
VALUES ('����� �1',1),('����� �1',1),('����������� �1',1),('����������� �2',1);

INSERT INTO DepartmentOperations ( DepartmentId,OperationId)
VALUES (1,2),(2,2),(3,1),(3,4),(4,1),(4,4);
