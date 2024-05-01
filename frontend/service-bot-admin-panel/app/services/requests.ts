
export interface Request{
    description: string,
    id: string
    status: string
}
export interface EmployeeRequest{
    id: string,
    login: string,
    fullName: string,
    department: string,
    phone: string
}
// export interface AproveRequest{
//     id: string
// }
export interface DeniedRequest{
    id: string,
    reason: string
}


export const getAllRequests = async () => {
    const response = await fetch("http://localhost:5105/RequestsForDays/GetActiveRequests")
    const result = await response.json();
    debugger;
    return result;
}

export const createEmployee = async (employeRequest: EmployeeRequest)=>{
    await fetch("http://localhost:5105/Employee/CreateEmploye", {
        method: "POST",
        headers: {
            "content-type": "application/json",
        },
        body: JSON.stringify(employeRequest)
    })
}

export const aproveRequest = async (id: string)=>{
    await fetch("http://localhost:5105/RequestsForDays/AproveRequest", {
        method: "PUT",
        headers: {
            "content-type": "application/json",
        },
        body: JSON.stringify(id)
    })
}

export const deniedRequest = async (employeRequest: DeniedRequest)=>{
    debugger;
    await fetch("http://localhost:5105/RequestsForDays/DeniedRequests", {
        method: "PUT",
        headers: {
            "content-type": "application/json",
        },
        body: JSON.stringify(employeRequest)
    })
}
export const getAllEmployes = async () => {
    const response = await fetch("http://localhost:5105/Employee/GetEmployes")
    const result = await response.json();
    debugger;
    return result;
}


// interface DataType {
//     id: string;
//     login: string;
//     fullName: string;
//     department: string;
//     isAutorized: string;
//     phone: string;
//   }