// interface Request {
//     $id : string,
//     createDate: string,
//     description: string,
//     employeFullName: string,
//     employeId: string,
//     endDate: string,
//     id: string,
//     requestStatus: string,
//     requestType: string,
//     responce: string,
//     startDate: string,
//     telegramChatId: string,
// }
interface Request {
    id: string;
    number: number;
    date: string;
    status: string;
    type: string;
    fio: string;
    period: string;
    description: string;
  }