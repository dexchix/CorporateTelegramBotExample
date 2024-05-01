import Modal from "antd/es/modal/Modal";
import { UpdateRequest, deniedRequest } from "../services/requests";
import { useEffect, useState } from "react";
import Input from "antd/es/input/Input";
import TextArea from "antd/es/input/TextArea";
import { UpdateMessage } from "next/dist/build/swc";

interface Props{
    mode: Mode;
    id: string;
    values: Request;
    isModalOpen: boolean;
    handleCancel: ()=> void;
    handleCreate: (aproveRequest: UpdateRequest) => void;
    handleUpdate: (deniedRequest: UpdateRequest) => void;
}

export enum Mode{
    Aprove,
    Denied,
}

export const AproveRequestForDays = ({
    mode,
    id,
    values,
    isModalOpen,
    handleCancel,
    handleCreate,
    handleUpdate,
}: Props) => {
    const[reason, setReason] = useState<string>("");

useEffect(() => {
 
}, [values])

    const handleOnOk = async () => {
        debugger;
        const deniedRequest = {id, reason}

        mode === Mode.Aprove ? handleCreate(deniedRequest) : handleUpdate(deniedRequest)
    }
        if(mode === Mode.Denied){
        return(
        <>
            <Modal 
                title= "Отказ"
                open={isModalOpen} 
                cancelText={"Отмена"}
                onOk={handleOnOk}
                onCancel={handleCancel}
            >
                    <div className="book_modal">
                        <TextArea style={{margin: 10}}
                            value={reason}
                            onChange={(e) => setReason(e.target.value)}
                            placeholder="Дополнительная информация"
                        />
                    </div>
            </Modal> 
        </>)
        }
        else{
            return(
            <>
            <Modal 
                title= "Подтверждение"
                open={isModalOpen} 
                cancelText={"Отмена"}
                onOk={handleOnOk}
                onCancel={handleCancel}
            >
            </Modal> 
        </>)

        }
    
};