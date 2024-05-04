"use client";

import React, { useEffect, useState } from 'react';
import { Button, Space, Table, Tag } from 'antd';
import type { TableProps } from 'antd';
import { getIncidents } from '../services/requests';


export default function IncidentsPage(){
  const[values, setValues] = useState<Incident>({
    id: "",
    number: "",
    date: "",
    fio: "",
    description: "",
} as Incident);

const [incidents, setIncidents] = useState<Incident[]>([]);

useEffect(() => {
  const getEmployes = async () => {
    const incidents = await getIncidents();
    setIncidents(incidents);
  };

  getEmployes(); // Вызываем функцию получения заявок
}, []); // Пустой массив зависимостей


    return (<Table columns={columns} dataSource={incidents} />);
}
const columns: TableProps<Incident>['columns'] = [
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