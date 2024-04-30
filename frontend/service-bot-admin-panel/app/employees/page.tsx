"use client";

import React from 'react';
import { Button, Space, Table, Tag } from 'antd';
import type { TableProps } from 'antd';

interface DataType {
    id: string;
    login: string;
    fullName: string;
    department: string;
    isAutorized: string;
    phone: string;
  }

  const columns: TableProps<DataType>['columns'] = [
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

  const data: DataType[] = [
    {
      id: '1',
      login: 'test',
      fullName: 'Иванов Иван Иванович',
      department: 'Отдел разработки программного обеспечения',
      isAutorized: 'Не авторизован',
      phone: '79803391975'
    },
    {
        id: '1',
        login: 'test1',
        fullName: 'Петров Башир Баширович',
        department: 'Отдел разработки программного обеспечения',
        isAutorized: 'Не авторизован',
        phone: '79803391975'
    },
    {
        id: '1',
        login: 'test2',
        fullName: 'Баширов Петр Петрович',
        department: 'Отдел разработки программного обеспечения',
        isAutorized: 'Не авторизирован',
        phone: '79803391975'
    },
  ];

export default function EmployeesPage(){
    return (
    <div>
        <Button style={{margin: 10}}>Добавить сотрудника</Button>
        <Table columns={columns} dataSource={data} />
    </div>);
}