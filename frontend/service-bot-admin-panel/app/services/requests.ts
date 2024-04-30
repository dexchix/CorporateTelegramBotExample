export interface Request{
    description: string,
    id: string
    status: string
}

export const getAllRequests = async () => {
    const response = await fetch("http://localhost:5105/RequestsForDays/GetActiveRequests")
    const result = await response.json();
    debugger;
    return result;
}