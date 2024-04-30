"use client";

import React, { useEffect, useState } from 'react';
import { Button, Space, Table, Tag } from 'antd';
import type { TableProps } from 'antd';
import { getAllRequests } from '../services/requests';





export default function RequestsPage() {
  const [requests, setRequests] = useState<Request[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const getRequests = async () => {
      const requests = await getAllRequests();
      setLoading(false);
      setRequests(requests);
    };

    getRequests(); // Вызываем функцию получения заявок
  }, []); // Пустой массив зависимостей

  return <Table columns={columns} dataSource={requests} />; // Подставьте свои данные для таблицы
}
  const columns: TableProps<Request>['columns'] = [
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
        title: 'Статус',
        dataIndex: 'status',
        key: 'status',
    },
    {
        title: 'Тип',
        dataIndex: 'type',
        key: 'type',
    },
    {
        title: 'ФИО',
        dataIndex: 'fio',
        key: 'fio',
    },
    {
      title: 'Период',
      dataIndex: 'period',
      key: 'period',
    },
    {
        title: 'Обоснование',
        dataIndex: 'description',
        key: 'description',
    },
    {
        title: '',
        key: 'actions',
        dataIndex: 'actions',
        render: () => (
            <>
            <Button style= {{backgroundColor:'green', color:'white'}} >Одобрить</Button>
            <Button style= {{backgroundColor:'red', color:'white'}} >Отказать</Button>
            </>

        ),
    },
  ];
  
  // const data: DataType[] = [
  //   {
  //     id: '1',
  //     number: 12323,
  //     status: 'Рассматривается',
  //     date: '11.10.2023',
  //     type: 'Отгул',
  //     fio: 'Иванов Иван Иванович',
  //     period: '10.10.2023 - 20.20.2024',
  //     description: 'Для добавления красной и зеленой кнопок в приложение, использующее Next.js 14.2.2, вы можете создать компоненты кнопок с соответствующими стилями. Вот пример того, как вы можете это сделать:',
  //   },
  //   {
  //     id: '2',
  //     number: 345645,
  //     status: 'Закрыто',
  //     date: '12.10.2023',
  //     type: 'Отпуск',
  //     fio: 'Петров Петр Петрович',
  //     period: '10.10.2023 - 20.20.2024',
  //     description: 'Для добавления красной и зеленой кнопок в приложение, использующее Next.js 14.2.2, вы можете создать компоненты кнопок с соответствующими стилями. Вот пример того, как вы можете это сделать:',
  //   },
  //   {
  //     id: '3',
  //     number: 756546,
  //     date: '13.10.2023',
  //     status: 'Отказано',
  //     type: 'Переработка',
  //     fio: 'Баширов Башир Баширович',
  //     period: '10.10.2023 - 20.20.2024',
  //     description: 'Для добавления красной и зеленой кнопок в приложение, использующее Next.js 14.2.2, вы можете создать компоненты кнопок с соответствующими стилями. Вот пример того, как вы можете это сделать:',
  //   },
  // ];









