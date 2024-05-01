"use client";

import React, { useEffect, useState } from 'react';
import { Button, Space, Table, Tag } from 'antd';
import type { TableProps } from 'antd';
import { DeniedRequest, aproveRequest, deniedRequest, getAllRequests } from '../services/requests';
import { AproveRequestForDays, Mode } from '../components/AproveRequest';

export default function RequestsPage() {
  const[values, setValues] = useState<Request>({
    id: "",
    number: "",
    date: "",
    status: "",
    type: "",
    fio: "",
    period: "",
    description: "",
  } as unknown as Request);


  const [requests, setRequests] = useState<Request[]>([]);
  const [loading, setLoading] = useState(true);
  const [isModalOpen, setModalOpen] = useState(false);
  const [mode, setMode] = useState(Mode.Aprove);

  useEffect(() => {
    const getRequests = async () => {
      const requests = await getAllRequests();
      setLoading(false);
      setRequests(requests);
    };

    getRequests(); // Вызываем функцию получения заявок
  }, []); // Пустой массив зависимостей

  const openAproveRequestModal = () => {
    debugger;
    setMode(Mode.Aprove);
    setModalOpen(true);
  }

  const openDeniedRequestModal = () => {
    debugger;
    setMode(Mode.Denied);
    setModalOpen(true);
  }

  const handleAproveRequest = async (id: string)=>{
    await aproveRequest(id);
    closeModal();

    debugger;
    const request = await getAllRequests();
    setRequests(request);
};

const handleDeniedRequest = async (id: string, request: DeniedRequest)=>{
    await deniedRequest(request);
    closeModal();

    const responce = await getAllRequests();
    setRequests(responce);
}

const openModal = () => {
    setMode(Mode.Aprove);
    setModalOpen(true);
};

const closeModal = () => 
  setModalOpen(false);


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
            <Button style= {{backgroundColor:'green', color:'white'}} onClick={openAproveRequestModal}>Одобрить</Button>
            <Button style= {{backgroundColor:'red', color:'white'}} onClick={openDeniedRequestModal}>Отказать</Button>
            </>

        ),
    },
];

  return (
    <>
    <AproveRequestForDays
                mode = {mode}
                values={values}
                isModalOpen = {isModalOpen}
                handleCreate={handleAproveRequest}
                handleUpdate={handleDeniedRequest}
                handleCancel={closeModal}
    />
  <Table columns={columns} dataSource={requests} /> 
    </>)
  
}

  


  








