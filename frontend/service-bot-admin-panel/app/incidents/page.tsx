import React, { useEffect, useState } from 'react';
import { Table, Select, Input, Button } from 'antd';
import { getAllRequests } from '../services/requests';

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
  } as Request);

  const [requests, setRequests] = useState<Request[]>([]);
  const [filteredRequests, setFilteredRequests] = useState<Request[]>([]);
  const [loading, setLoading] = useState(true);

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

    getRequests();
  }, []);

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

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>, key: string) => {
    setValues({ ...values, [key]: e.target.value });
  };

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
  ];

  return (
    <>
      <div style={styles.filterContainer}>
        <Input placeholder="Дата" value={dateFilter} onChange={(e) => setDateFilter(e.target.value)} style={{ width: 200 }} />
        <Select defaultValue="" style={{ width: 200 }} placeholder="Статус" onChange={(value) => setStatusFilter(value)}>
          <Option value="Одобрено">Одобрено</Option>
          <Option value="На рассмотрении">На рассмотрении</Option>
          <Option value="Отклонено">Отклонено</Option>
        </Select>
        <Select defaultValue="" style={{ width: 200 }} placeholder="Тип" onChange={(value) => setTypeFilter(value)}>
          <Option value="Отпуск">Отпуск</Option>
          <Option value="Больничный">Больничный</Option>
          <Option value="Отгул">Отгул</Option>
        </Select>
        <Input placeholder="ФИО" value={fioFilter} onChange={(e) => setFioFilter(e.target.value)} style={{ width: 200 }} />
        <Input placeholder="Период" value={periodFilter} onChange={(e) => setPeriodFilter(e.target.value)} style={{ width: 200 }} />
        <Button style={{ margin: 10 }} onClick={() => setFilteredRequests(requests)}>Применить фильтры</Button>
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