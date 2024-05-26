"use client";

import React, { useEffect, useState } from 'react';
import { Button, Table, Select, Typography, Input } from 'antd';
import { UpdateRequest, aproveRequest, deniedRequest, getAllRequests } from '../services/requests';
import { AproveRequestForDays, Mode } from '../components/AproveRequest';

const { Option } = Select;

export default function RequestsPage() {
  const [values, setValues] = useState<Request>({
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
  const [filteredRequests, setFilteredRequests] = useState<Request[]>([]);
  const [loading, setLoading] = useState(true);
  const [isModalOpen, setModalOpen] = useState(false);
  const [mode, setMode] = useState(Mode.Aprove);
  const [id, setId] = useState<string>("324234234");

  const [statusFilter, setStatusFilter] = useState('');
  const [typeFilter, setTypeFilter] = useState('');
  const [fioFilter, setFioFilter] = useState('');
  const [periodFilter, setPeriodFilter] = useState('');
  const [dateFilter, setDateFilter] = useState('');

  useEffect(() => {
    const getRequests = async () => {
      const requests = await getAllRequests();
      setLoading(false);
      setRequests(requests);
      setFilteredRequests(requests);
    };

    getRequests(); // Вызываем функцию получения заявок
  }, []); // Пустой массив зависимостей

  useEffect(() => {
    let filtered = [...requests];

    if (statusFilter) {
      filtered = filtered.filter(request => request.status === statusFilter);
    }

    if (typeFilter) {
      filtered = filtered.filter(request => request.type === typeFilter);
    }

    if (fioFilter) {
      filtered = filtered.filter(request => request.fio.toLowerCase().includes(fioFilter.toLowerCase()));
    }

    if (periodFilter) {
      filtered = filtered.filter(request => request.period.toLowerCase().includes(periodFilter.toLowerCase()));
    }

    if (dateFilter) {
      filtered = filtered.filter(request => request.date.toLowerCase().includes(dateFilter.toLowerCase()));
    }

    setFilteredRequests(filtered);
  }, [statusFilter, typeFilter, fioFilter, periodFilter, dateFilter, requests]);

  const openAproveRequestModal = (request: Request) => {
    setMode(Mode.Aprove);
    setId(request.id);
    setModalOpen(true);
  }

  const openDeniedRequestModal = (request: Request) => {
    setMode(Mode.Denied);
    setId(request.id);
    setModalOpen(true);
  }

  const handleAproveRequest = async (request: UpdateRequest) => {
    await aproveRequest(request);
    closeModal();

    const response = await getAllRequests();
    setRequests(response);
  };

  const handleDeniedRequest = async (request: UpdateRequest) => {
    await deniedRequest(request);
    closeModal();

    const response = await getAllRequests();
    setRequests(response);
  }

  const openModal = () => {
    setMode(Mode.Aprove);
    setModalOpen(true);
  };

  const closeModal = () => setModalOpen(false);

  const columns = [
    {
      title: 'Номер',
      dataIndex: 'number',
      key: 'number',
      render: (text: string) => <a>{text}</a>,
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
      render: (text: string, record: Request) => (
        <>
          <Button style={{ backgroundColor: 'green', color: 'white' }} onClick={() => openAproveRequestModal(record)}>Одобрить</Button>
          <Button style={{ backgroundColor: 'red', color: 'white' }} onClick={() => openDeniedRequestModal(record)}>Отказать</Button>
        </>
      ),
    },
  ];

  return (
    <>
      <AproveRequestForDays
        mode={mode}
        id={id}
        values={values}
        isModalOpen={isModalOpen}
        handleCreate={handleAproveRequest}
        handleUpdate={handleDeniedRequest}
        handleCancel={closeModal}
      />
      <div style={styles.filterContainer}>
        <Input placeholder="Дата" value={dateFilter} onChange={(e) => setDateFilter(e.target.value)} style={{ width: 200 }} />
        <Select defaultValue="" style={{ width: 200, height: 50 }} placeholder="Статус" onChange={(value) => setStatusFilter(value)}>
          <Option value="Одобрено">Одобрено</Option>
          <Option value="На рассмотрении">На рассмотрении</Option>
          <Option value="Отклонено">Отклонено</Option>
        </Select>
        <Select defaultValue="" style={{ width: 200 , height: 50 }} placeholder="Тип" onChange={(value) => setTypeFilter(value)}>
          <Option value="Отпуск">Отпуск</Option>
          <Option value="Больничный">Больничный</Option>
          <Option value="Отгул">Отгул</Option>
        </Select>
        <Input placeholder="ФИО" value={fioFilter} onChange={(e) => setFioFilter(e.target.value)} style={{ width: 200 }} />
        <Input placeholder="Период" value={periodFilter} onChange={(e) => setPeriodFilter(e.target.value)} style={{ width: 200 }} />
        <Button style={{ margin: 10  }} onClick={() => setFilteredRequests(requests)}>Применить фильтры</Button>
      </div>
      <Table columns={columns} dataSource={filteredRequests} loading={loading} style={{ width: '100%', textAlign: 'center' }} />
    </>
  );
}

const styles = {
  filterContainer: {
    marginTop: '20px',
    marginBottom: '20px',
    display: 'flex',
    justifyContent: 'center',
    gap: '10px',
  }
};
