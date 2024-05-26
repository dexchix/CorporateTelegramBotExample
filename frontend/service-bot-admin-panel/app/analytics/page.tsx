"use client";

import React from 'react';
import { Button, Space, Layout } from 'antd';
import { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, Legend, BarChart, Bar, PieChart, Pie, Cell } from 'recharts';

const { Header, Content } = Layout;

const data = [
  { name: 'Page A', Мужчины: 4000, Женщины: 2400, amt: 2400, Отгулы: 4000, Переработки: 2400 },
  { name: 'Page B', Мужчины: 3000, Женщины: 1398, amt: 2210, Отгулы: 3000, Переработки: 1398 },
  { name: 'Page C', Мужчины: 2000, Женщины: 9800, amt: 2290, Отгулы: 2000, Переработки: 9800 },
  { name: 'Page D', Мужчины: 2780, Женщины: 3908, amt: 2000, Отгулы: 2780, Переработки: 3908 },
  { name: 'Page E', Мужчины: 1890, Женщины: 4800, amt: 2181, Отгулы: 1890, Переработки: 4800 },
  { name: 'Page F', Мужчины: 2390, Женщины: 3800, amt: 2500, Отгулы: 2390, Переработки: 3800 }
];

const pieData = [
  { name: 'Отдел фронтенд разработки', value: 400 },
  { name: 'Отдел бекенд разработки', value: 300 },
  { name: 'Отдел мобильной разработки', value: 300 },
  { name: 'Отдел веб-дизайна', value: 200 },
];

const COLORS = ['#0088FE', '#00C49F', '#FFBB28', '#FF8042'];

const IncidentsPage = () => {
  return (
    <Layout style={{ minHeight: '100vh' }}>
      <Header style={{ color: '#fff', textAlign: 'center', fontSize: '24px' }}>Аналитика</Header>
      <Content style={{ padding: '20px 50px', display: 'flex', flexDirection: 'column', flex: 1 }}>
      <div style={{ display: 'flex', justifyContent: 'center', padding: '10px 0', background: '#fff' }}>
          <Space>
            <Button type="primary">Отчет по отпускам</Button>
            <Button type="primary">Отчет по отгулам</Button>
            <Button type="primary">Отчет по переработкам</Button>
          </Space>
        </div>
        <div style={{ flex: 1, display: 'flex', flexWrap: 'wrap', justifyContent: 'space-around', alignItems: 'center' }}>
          <div style={{ width: '40%', height: '40%' }}>
            <LineChart width={350} height={250} data={data}>
              <CartesianGrid strokeDasharray="3 3" />
              <XAxis dataKey="name" />
              <YAxis />
              <Tooltip />
              <Legend />
              <Line type="monotone" dataKey="Женщины" stroke="#8884d8" />
              <Line type="monotone" dataKey="Мужчины" stroke="#82ca9d" />
            </LineChart>
          </div>
          <div style={{ width: '40%', height: '40%' }}>
            <BarChart width={350} height={250} data={data}>
              <CartesianGrid strokeDasharray="3 3" />
              <XAxis dataKey="name" />
              <YAxis />
              <Tooltip />
              <Legend />
              <Bar dataKey="Отгулы" fill="#8884d8" />
              <Bar dataKey="Переработки" fill="#82ca9d" />
            </BarChart>
          </div>
          <div style={{ width: '40%', height: '40%' }}>
            <PieChart width={350} height={250}>
              <Pie
                data={pieData}
                cx={175}
                cy={125}
                innerRadius={60}
                outerRadius={80}
                fill="#8884d8"
                paddingAngle={5}
                dataKey="value"
              >
                {pieData.map((entry, index) => (
                  <Cell key={`cell-${index}`} fill={COLORS[index % COLORS.length]} />
                ))}
              </Pie>
              <Tooltip />
              <Legend />
            </PieChart>
          </div>
          <div style={{ width: '40%', height: '40%' }}>
            <PieChart width={350} height={250}>
              <Pie
                data={pieData}
                cx={175}
                cy={125}
                outerRadius={60}
                fill="#8884d8"
                dataKey="value"
              >
                {pieData.map((entry, index) => (
                  <Cell key={`cell-${index}`} fill={COLORS[index % COLORS.length]} />
                ))}
              </Pie>
              <Tooltip />
              <Legend />
            </PieChart>
          </div>
        </div>
      </Content>
    </Layout>
  );
};

export default IncidentsPage;