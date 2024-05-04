
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
export interface UpdateRequest{
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

export const aproveRequest = async (request: UpdateRequest)=>{
    debugger;
    const requestFixed = {id: request.id}
    await fetch("http://localhost:5105/RequestsForDays/AproveRequest", {
        method: "PUT",
        headers: {
            "content-type": "application/json",
        },
        body: JSON.stringify(requestFixed)
    })
}

export const deniedRequest = async (request: UpdateRequest)=>{
  
    const requestFixed = {id: request.id, reason: request.reason}
    await fetch("http://localhost:5105/RequestsForDays/DeniedRequests", {
        method: "PUT",
        headers: {
            "content-type": "application/json",
        },
        body: JSON.stringify(requestFixed)
    })
}
export const getAllEmployes = async () => {
    debugger;

    const response = await fetch("http://localhost:5105/Employee/GetEmployes")
    const result = await response.json();
    return result;
}
export const getIncidents = async () => {
    const response = await fetch("http://localhost:5105/Incident/GetIncidents")
    const result = await response.json();
    return result;
}
