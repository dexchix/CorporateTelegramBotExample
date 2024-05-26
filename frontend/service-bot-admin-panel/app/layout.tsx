import { Button, Layout, Menu } from "antd";
import { Content, Footer, Header } from "antd/es/layout/layout";
import Link from "next/link";


const items = [
  {key: "Заявки", label: <Link href={"/requests"}>Заявки</Link>}, 
  {key: "Инциденты", label: <Link href={"/incidents"}>Инциденты</Link>}, 
  {key: "Сотрудники", label: <Link href={"/employees"}>Работники</Link>}, 
  {key: "Новости и мероприятия", label: <Link href={"/events"}>Новости и мероприятия</Link>},
  {key: "Аналитика", label: <Link href={"/analytics"}>Аналитика</Link>},  
  {key: "Отчеты", label: <Link href={"/reports"}>Отчеты</Link>},  
]

export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <html lang="en">
      <body>
        <Layout style={{ minHeight: "100hv" }}>
          <Header style={{ display: "flex", justifyContent: "space-between", alignItems: "center" }}>
            <Menu 
              theme="dark" 
              mode="horizontal" 
              items={items} 
              style={{ flex: 1, minWidth: 0 }}
            />
            <Button>Бубало Д.А.</Button>
          </Header>
          <Content style={{ padding: "0 48px" }}>{children}</Content>
          <Footer style={{ textAlign: "center", position: "fixed", bottom: 0, width: "100%" }}>
            Service bot admin panel. Created by Denis Bubalo
          </Footer>
        </Layout>
      </body>
    </html>
  );
}
