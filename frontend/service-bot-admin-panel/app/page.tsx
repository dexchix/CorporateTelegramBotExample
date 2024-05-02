"use client";

// import Image from "next/image";
// import styles from "./page.module.css";
// import { Tag, Input, Button } from "antd";
// import { useState } from "react";

// export default function Home() {
//     const[login, setLogin] = useState<string>("");
//     const[password, setPassword] = useState<string>("");

//    return (
//       <div className="autorization">
//       <Tag style={{border: 0, }}>Добавьте пост</Tag>
//          <Input value={login} 
//             onChange={(e) => setLogin(e.target.value)}
//             placeholder="Логин" >Логин
//          </Input> 
//       <Input value={password} 
//             onChange={(e) => setPassword(e.target.value)}
//             placeholder="пароль" >Пароль</Input>
//       <Button>Войти</Button>
//       </div>
//   )

// }


import Image from "next/image";
import styles from "./page.module.css";
import { Tag, Input, Button } from "antd";
import { useState } from "react";

export default function Home() {
    const [login, setLogin] = useState<string>("");
    const [password, setPassword] = useState<string>("");

    return (
<div style={{ display: "flex", flexDirection: "column", justifyContent: "center", alignItems: "center", height: "100vh" }}>
      <Tag  style={{ border: 0, fontSize: "20px", marginBottom: "10px" }}>Авторизация</Tag>
      <Input
        style={{ width: "200px", marginBottom: "10px" }}
        value={login}
        onChange={(e) => setLogin(e.target.value)}
        placeholder="Логин"
      />
      <Input.Password
        style={{ width: "200px", marginBottom: "10px" }}
        value={password}
        onChange={(e) => setPassword(e.target.value)}
        placeholder="Пароль"
      />
      <Button style={{ width: "200px", marginBottom: "10px" }}>Войти</Button>
    </div>
    );
}