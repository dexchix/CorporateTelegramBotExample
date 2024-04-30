import Modal from "antd/es/modal/Modal";
import { AproveRequest } from "../services/requests";
import { useEffect, useState } from "react";
import Input from "antd/es/input/Input";
import TextArea from "antd/es/input/TextArea";

interface Props{
    mode: Mode;
    values: Request;
    isModalOpen: boolean;
    handleCancel: ()=> void;
    handleCreate: (request: AproveRequest) => void;
    handleUpdate: (id: string, request: AproveRequest) => void;
}

export enum Mode{
    Aprove,
    Denied,
}

export const AproveRequestForDays = ({
    mode,
    values,
    isModalOpen,
    handleCancel,
    handleCreate,
    handleUpdate,
}: Props) => {
    const[id, setId] = useState<string>("");
    const[reason, setReason] = useState<string>("");

useEffect(() => {
     setId(values.id);
}, [values])

    const handleOnOk = async () => {
        const aproveRequest = {id};

        mode === Mode.Aprove ? handleCreate(aproveRequest) : handleUpdate(values.id, aproveRequest)
    }
    // debugger;
    return (
    <>
        <Modal 
            title={mode === Mode.Aprove ? "Подтверждение" : "Отказ"}
            open={isModalOpen} 
            cancelText={"Отмена"}
            onOk={handleOnOk}
            onCancel={handleCancel}
        >
                <div className="book_modal">
                    <TextArea style={{margin: 10}}
                        value={reason}
                        onChange={(e) => setReason(e.target.value)}
                        placeholder="Логин"
                    />
                </div>
        </Modal> 
    </>)
};