"use client";

import React from 'react';
import { Button, Space, Table, Tag } from 'antd';
import type { TableProps } from 'antd';

interface DataType {
    id: string;
    number: number;
    date: string;
    fio: string;
    description: string;
  }

  const columns: TableProps<DataType>['columns'] = [
    {
      title: 'Номер',
      dataIndex: 'number',
      key: 'number',
      render: (text) => <a>{text}</a>,
    },
    {
      title: 'Дата',
      dataIndex: 'date',
      key: 'date',
    },
    {
        title: 'ФИО',
        dataIndex: 'fio',
        key: 'fio',
    },
    {
        title: 'Описание',
        dataIndex: 'description',
        key: 'description',
    }
  ];
  
  const data: DataType[] = [
    {
      id: '1',
      number: 12323,
      date: '11.10.2023',
      fio: 'Иванов Иван Иванович',
      description: 'Для добавления красной и зеленой кнопок в приложение, использующее Next.js 14.2.2, вы можете создать компоненты кнопок с соответствующими стилями. Вот пример того, как вы можете это сделать:',
    },
    {
      id: '2',
      number: 345645,
      date: '12.10.2023',
      fio: 'Петров Петр Петрович',
      description: 'Для добавления красной и зеленой кнопок в приложение, использующее Next.js 14.2.2, вы можете создать компоненты кнопок с соответствующими стилями. Вот пример того, как вы можете это сделать:',
    },
    {
      id: '3',
      number: 756546,
      date: '13.10.2023',
      fio: 'Баширов Башир Баширович',
      description: 'Для добавления красной и зеленой кнопок в приложение, использующее Next.js 14.2.2, вы можете создать компоненты кнопок с соответствующими стилями. Вот пример того, как вы можете это сделать:',
    },
  ];

export default function RequestsPage(){
    return <Table columns={columns} dataSource={data} />
}
