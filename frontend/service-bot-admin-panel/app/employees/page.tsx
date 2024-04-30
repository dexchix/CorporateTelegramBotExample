"use client";

import React, { useEffect, useState } from 'react';
import { Button, Space, Table, Tag } from 'antd';
import type { TableProps } from 'antd';
import { EmployeeRequest, createEmployee, getAllEmployes } from '../services/requests';
import { CreateUpdateEmploye, Mode } from '../components/CreateUpdateEmploye';
import { open } from 'fs/promises';

export default function EmployeesPage(){
    // const [employes, setEmployes] = useState<Employee[]>([]);
    // const [loading, setLoading] = useState(true);

    const[values, setValues] = useState<Employee>({
        id: "",
        login: "",
        fullName: "",
        department: "",
        isAutorized: "",
        phone: "",
} as Employee);

    const [employes, setEmployes] = useState<Employee[]>([]);
    const [loading, setLoading] = useState(true);
    const [isModalOpen, setModalOpen] = useState(false);
    const [mode, setMode] = useState(Mode.Create);

    useEffect(() => {
        const getEmployes = async () => {
          const employes = await getAllEmployes();
          setLoading(false);
          setEmployes(employes);
        };
    
        getEmployes(); // Вызываем функцию получения заявок
      }, []); // Пустой массив зависимостей


        const handleCreateEmploye = async (request: EmployeeRequest) => {
            await createEmployee(request);
            closeModal();

            const employes = await getAllEmployes();
            setEmployes(employes);
        };

        const handleUpdateEmploye = async (id: string, request: EmployeeRequest)=>{
            await createEmployee(request);
            closeModal();

            debugger;
            const employes = await getAllEmployes();
            setEmployes(employes);
        };

        const handleDeleteEmployee = async (id: string, request: EmployeeRequest)=>{
            await createEmployee(request);
            closeModal();

            const employes = await getAllEmployes();
            setEmployes(employes);
        }

        const openModal = () => {
            setMode(Mode.Create);
            setModalOpen(true);
        };

        const closeModal = () => {
          setModalOpen(false);

        const openEditModal = (employee: Employee) => {
            setMode(Mode.Edit);
            setValues(employee);
            setModalOpen(true);
        }
    };

    return (
        <div>
            <Button style={{margin: 10}} onClick={openModal
            }>Добавить сотрудника</Button>
                <CreateUpdateEmploye
                mode = {mode}
                values={values}
                isModalOpen = {isModalOpen}
                handleCreate={handleCreateEmploye}
                handleUpdate={handleUpdateEmploye}
                handleCancel={closeModal}
                />
            <Table columns={columns} dataSource={employes}/>
        </div>);
}

  const columns: TableProps<Employee>['columns'] = [
    {
        title: 'Логин',
        dataIndex: 'login',
        key: 'login',
    },
    {
        title: 'ФИО',
        dataIndex: 'fullName',
        key: 'fullName',
    },
    {
        title: 'Отдел',
        dataIndex: 'department',
        key: 'department',
    },
    {
        title: 'Авторизован',
        dataIndex: 'isAutorized',
        key: 'isAutorized',
    },
    {
        title: 'Номер телефона',
        dataIndex: 'phone',
        key: 'phone',
    }
  ];

//   const data: DataType[] = [
//     {
//       id: '1',
//       login: 'test',
//       fullName: 'Иванов Иван Иванович',
//       department: 'Отдел разработки программного обеспечения',
//       isAutorized: 'Не авторизован',
//       phone: '79803391975'
//     },
//     {
//         id: '1',
//         login: 'test1',
//         fullName: 'Петров Башир Баширович',
//         department: 'Отдел разработки программного обеспечения',
//         isAutorized: 'Не авторизован',
//         phone: '79803391975'
//     },
//     {
//         id: '1',
//         login: 'test2',
//         fullName: 'Баширов Петр Петрович',
//         department: 'Отдел разработки программного обеспечения',
//         isAutorized: 'Не авторизирован',
//         phone: '79803391975'
//     },
//   ];

