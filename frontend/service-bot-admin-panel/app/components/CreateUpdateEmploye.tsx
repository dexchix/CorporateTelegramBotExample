import Modal from "antd/es/modal/Modal";
import { EmployeeRequest } from "../services/requests";
import { useEffect, useState } from "react";
import Input from "antd/es/input/Input";

interface Props{
    mode: Mode;
    values: Employee;
    isModalOpen: boolean;
    handleCancel: ()=> void;
    handleCreate: (request: EmployeeRequest) => void;
    handleUpdate: (id: string, request: EmployeeRequest) => void;
}

export enum Mode{
    Create,
    Update,
    Edit
}

export const CreateUpdateEmploye = ({
    mode,
    values,
    isModalOpen,
    handleCancel,
    handleCreate,
    handleUpdate,
}: Props) => {
    const[id, setId] = useState<string>("");
    const[login, setLogin] = useState<string>("");
    const[fullName, setFullName] = useState<string>("");
    const[department, setDepartment] = useState<string>("");
    const[isAutorized, setIsAutorized] = useState<string>("");
    const[phone, setPhone] = useState<string>("");

useEffect(() => {
    setLogin(values.login);
     setFullName(values.fullName);
     setDepartment(values.department);
     setPhone(values.phone);
}, [values])

    const handleOnOk = async () => {
        const employeRequest = {id, login, fullName, department, phone};

        mode === Mode.Create ? handleCreate(employeRequest) : handleUpdate(values.id, employeRequest)
    }
    // debugger;
    return (
    <>
        <Modal 
        title={mode === Mode.Create ? "Добавить сотрудника" : "Редактировать сотрудника"}
        open={isModalOpen} 
        cancelText={"Отмена"}
        onOk={handleOnOk}
        onCancel={handleCancel}
        >
            <div className="book_modal">
                <Input style={{margin: 10}}
                    value={login}
                    onChange={(e) => setLogin(e.target.value)}
                    placeholder="Логин"
                />
                <Input style={{margin: 10}}
                    value={fullName}
                    onChange={(e) => setFullName(e.target.value)}
                    placeholder="ФИО"
                />
                <Input style={{margin: 10}}
                    value={department}
                    onChange={(e) => setDepartment(e.target.value)}
                    placeholder="Отдел"
                />
                <Input style={{margin: 10}}
                    value={phone}
                    onChange={(e) => setPhone(e.target.value)}
                    placeholder="Телефон"
                />
            </div>
        </Modal> 
    </>)
};