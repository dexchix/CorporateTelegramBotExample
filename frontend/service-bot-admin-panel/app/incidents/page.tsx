"use client";

import React, { useEffect, useState } from 'react';
import { Button, Space, Table, Tag, Select } from 'antd';
import type { TableProps } from 'antd';
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

  const filterIncidents = (value: string, filterKey: string) => {
    if (value === "") {
      setFilteredIncidents(incidents);
    } else {
      const filtered = incidents.filter(incident => incident[filterKey] === value);
      setFilteredIncidents(filtered);
    }
  };

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

  return (
    <>
      <div style={styles.filterContainer}>
        <Select defaultValue="" style={{ width: 200 }} onChange={(value) => filterIncidents(value, 'fio')}>
          <Option value="">Фильтр по ФИО</Option>
          <Option value="Иванов Иван Иванович">Иванов Иван Иванович</Option>
          <Option value="Петров Петр Петрович">Петров Петр Петрович</Option>
          {/* Другие значения */}
        </Select>
        <Select defaultValue="" style={{ width: 200 }} onChange={(value) => filterIncidents(value, 'date')}>
          <Option value="">Фильтр по дате</Option>
          <Option value="2024-01-01">2024-01-01</Option>
          <Option value="2024-02-01">2024-02-01</Option>
          {/* Другие значения */}
        </Select>
        {/* Добавьте другие фильтры по необходимости */}
      </div>
      <Table columns={columns} dataSource={filteredIncidents} />
    </>
  );
}

const styles = {
  filterContainer: {
    marginBottom: '20px',
    display: 'flex',
    justifyContent: 'center',
    gap: '10px',
  }
};