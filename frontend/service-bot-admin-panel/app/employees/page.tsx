"use client";

import React, { useEffect, useState } from 'react';
import { Button, Table, Select, Input } from 'antd';
import { EmployeeRequest, createEmployee, getAllEmployes } from '../services/requests';
import { CreateUpdateEmploye, Mode } from '../components/CreateUpdateEmploye';

const { Option } = Select;

export default function EmployeesPage() {
    const [values, setValues] = useState<Employee>({
        id: "",
        login: "",
        fullName: "",
        department: "",
        isAutorized: "",
        phone: "",
    });

    const [employes, setEmployes] = useState<Employee[]>([]);
    const [filteredEmployes, setFilteredEmployes] = useState<Employee[]>([]);
    const [loading, setLoading] = useState(true);
    const [isModalOpen, setModalOpen] = useState(false);
    const [mode, setMode] = useState(Mode.Create);
    const [searchValue, setSearchValue] = useState("");
    const [departmentFilter, setDepartmentFilter] = useState("");
    const [authorizedFilter, setAuthorizedFilter] = useState("");

    useEffect(() => {
        const fetchEmployes = async () => {
            const employesData = await getAllEmployes();
            setEmployes(employesData);
            setFilteredEmployes(employesData);
            setLoading(false);
        };

        fetchEmployes();
    }, []);

    const filterEmployes = () => {
        let filteredData = employes;

        if (searchValue) {
            filteredData = filteredData.filter(employee => 
                employee.fullName.toLowerCase().includes(searchValue.toLowerCase())
            );
        }

        if (departmentFilter) {
            filteredData = filteredData.filter(employee => 
                employee.department.toLowerCase().includes(departmentFilter.toLowerCase())
            );
        }

        if (authorizedFilter) {
            filteredData = filteredData.filter(employee => employee.isAutorized === authorizedFilter);
        }

        setFilteredEmployes(filteredData);
    };

    const handleCreateEmploye = async (request: EmployeeRequest) => {
        await createEmployee(request);
        closeModal();
        const employesData = await getAllEmployes();
        setEmployes(employesData);
        setFilteredEmployes(employesData);
    };

    const openModal = () => {
        setMode(Mode.Create);
        setModalOpen(true);
    };

    const closeModal = () => {
        setModalOpen(false);
        setValues({
            id: "",
            login: "",
            fullName: "",
            department: "",
            isAutorized: "",
            phone: "",
        });
    };

    const openEditModal = (employee: Employee) => {
        setMode(Mode.Edit);
        setValues(employee);
        setModalOpen(true);
    };

    return (
        <div>
            <div style={styles.filterContainer}>
                <Input placeholder="Поиск по ФИО" value={searchValue} onChange={(e) => setSearchValue(e.target.value)} style={{ width: 200 }} />
                <Input placeholder="Фильтр по отделу" value={departmentFilter} onChange={(e) => setDepartmentFilter(e.target.value)} style={{ width: 200 }} />
                <Select placeholder="Фильтр по авторизации" style={{ width: 200 , height: 50}} onChange={(value) => setAuthorizedFilter(value)}>
                    <Option value="">Все</Option>
                    <Option value="Да">Да</Option>
                    <Option value="Нет">Нет</Option>
                </Select>
                <Button style={{ margin: 10 }} onClick={filterEmployes}>Применить фильтры</Button>
                <Button style={{ margin: 10 }} onClick={openModal}>Добавить сотрудника</Button>
            </div>
            <CreateUpdateEmploye
                mode={mode}
                values={values}
                isModalOpen={isModalOpen}
                handleCreate={handleCreateEmploye}
                handleCancel={closeModal}
            />
            <Table columns={columns} dataSource={filteredEmployes} loading={loading} />
        </div>
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

const columns = [
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