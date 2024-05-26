"use client";

import React from 'react';
import { Button } from 'antd';

export default function ReportsPage() {
  // Функции для создания PDF и Excel отчетов
  const generatePDFReport = (reportNumber) => {
    console.log(`Generating PDF report ${reportNumber}`);
    // Ваш код для создания PDF отчета
  };

  const generateExcelReport = (reportNumber) => {
    console.log(`Generating Excel report ${reportNumber}`);
    // Ваш код для создания Excel отчета
  };

  return (
    <div style={styles.container}>
      <h1 style={styles.title}>Отчеты</h1>
      <div style={styles.buttonContainer}>
        <Button type="primary" onClick={() => generatePDFReport(1)} style={styles.button}>
          Список сотрудников в Exel
        </Button>
        <Button type="primary" onClick={() => generatePDFReport(2)} style={styles.button}>
          Список активных заявок в Exel
        </Button>
        <Button type="primary" onClick={() => generatePDFReport(3)} style={styles.button}>
          Список закрытых заявок в Exel
        </Button>
        <Button type="primary" onClick={() => generatePDFReport(4)} style={styles.button}>
          Список одобренных заявок в Exel
        </Button>
        <Button type="primary" onClick={() => generatePDFReport(5)} style={styles.button}>
          Список неодобренных заявок в Exel
        </Button>
        <Button type="primary" onClick={() => generatePDFReport(6)} style={styles.button}>
          Список инцидентов в Exel
        </Button>
        <Button type="primary" onClick={() => generatePDFReport(6)} style={styles.button}>
          Создать заявление по Id в PDF
        </Button>
        <Button type="primary" onClick={() => generatePDFReport(6)} style={styles.button}>
          Отчет - Разница между отгулами и переработками в PDF
        </Button>
        <Button type="primary" onClick={() => generatePDFReport(6)} style={styles.button}>
          Отчет - Разница между отгулами и переработками в PDF
        </Button>
        <Button type="primary" onClick={() => generatePDFReport(6)} style={styles.button}>
          Отчет - Переработки в PDF
        </Button>
        <Button type="primary" onClick={() => generatePDFReport(6)} style={styles.button}>
          Отчет - Больничные в PDF
        </Button>
        <Button type="primary" onClick={() => generatePDFReport(6)} style={styles.button}>
          Отчет - Отпуска в PDF
        </Button>
        <Button type="primary" onClick={() => generatePDFReport(6)} style={styles.button}>
          Отчет - Работающие и не работающие сотрудники в PDF
        </Button>
      </div>
    </div>
  );
}

const styles = {
  container: {
    margin: '20px',
    padding: '20px',
    border: '1px solid #e8e8e8',
    borderRadius: '8px',
    backgroundColor: '#f9f9f9',
    textAlign: 'center',
    minHeight: '100vh', // Для вертикального выравнивания
    display: 'flex',
    flexDirection: 'column',
    justifyContent: 'center',
    alignItems: 'center',
  },
  title: {
    margin: '-400px 20px 20px 20px',
    fontSize: '24px',
    marginBottom: '20px',
  },
  buttonContainer: {
    margin: '200px 20px 20px 20px',
    display: 'flex',
    flexDirection: 'column',
    gap: '10px',
    width: '100%', // Для растягивания кнопок по ширине контейнера
    alignItems: 'center',
  },
  button: {
    width: '90%', // Ширина кнопок увеличена
    maxWidth: '900px', // Максимальная ширина кнопок
  }
};