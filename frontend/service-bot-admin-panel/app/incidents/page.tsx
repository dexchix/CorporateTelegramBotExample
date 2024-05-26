"use client";

import React, { useEffect, useState } from 'react';
import { Table, Select, Input, Button } from 'antd';
import { getIncidents } from '../services/requests';

const { Option } = Select;

export default function IncidentsPage() {
  const [values, setValues] = useState<Incident>({
    id: "",
    number: "",
    date: "",
    fio: "",
    description: "",
  } as Incident);

  const [incidents, setIncidents] = useState<Incident[]>([]);
  const [filteredIncidents, setFilteredIncidents] = useState<Incident[]>([]);

  useEffect(() => {
    const fetchIncidents = async () => {
      const incidentsData = await getIncidents();
      setIncidents(incidentsData);
      setFilteredIncidents(incidentsData);
    };

    fetchIncidents();
  }, []);

  const filterIncidents = () => {
    const filtered = incidents.filter(incident => {
      return incident.fio.toLowerCase().includes(values.fio.toLowerCase()) &&
             incident.date.includes(values.date);
    });
    setFilteredIncidents(filtered);
  };

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

  return (
    <>
      <div style={styles.filterContainer}>
        <Input placeholder="ФИО" value={values.fio} onChange={(e) => handleInputChange(e, 'fio')} style={{ width: 200 }} />
        <Input placeholder="Дата" value={values.date} onChange={(e) => handleInputChange(e, 'date')} style={{ width: 200 }} />
        <Button  onClick={filterIncidents}>Применить фильтры</Button>
      </div>
      <Table columns={columns} dataSource={filteredIncidents} />
    </>
  );
}

const styles = {
  filterContainer: {
    marginBottom: '20px',
    marginTop: '20px',
    display: 'flex',
    justifyContent: 'center',
    gap: '10px',
  }
};