import { Layout, Menu } from "antd";
import { Content, Footer, Header } from "antd/es/layout/layout";
import Link from "next/link";


const items = [
  {key: "Заявки", label: <Link href={"/requests"}>Заявки</Link>}, 
  {key: "Инциденты", label: <Link href={"/incidents"}>Инциденты</Link>}, 
  {key: "Сотрудники", label: <Link href={"/employees"}>Работники</Link>}, 
  {key: "Новости и мероприятия", label: <Link href={"/events"}>Новости и мероприятия</Link>}, 
]

export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <html lang="en">
      <body>
        <Layout style={{minHeight: "100hv"}}>
          <Header>
            <Menu 
               theme="dark" 
               mode="horizontal" 
               items = {items} 
               style = {{flex: 1, minValue: 0}}/>
           </Header>
           <Content style={{padding: "0 48px"}}>{children}</Content>
           <Footer style={{textAlign: "center"}}>
              Service bot admin panel. Created by Denis Bubalo
           </Footer>
      </Layout>
      </body>
    </html>
  );
}
