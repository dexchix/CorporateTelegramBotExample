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
          PDF Отчет 1
        </Button>
        <Button type="primary" onClick={() => generatePDFReport(2)} style={styles.button}>
          PDF Отчет 2
        </Button>
        <Button type="primary" onClick={() => generatePDFReport(3)} style={styles.button}>
          PDF Отчет 3
        </Button>
        <Button type="primary" onClick={() => generatePDFReport(4)} style={styles.button}>
          PDF Отчет 4
        </Button>
        <Button type="primary" onClick={() => generatePDFReport(5)} style={styles.button}>
          PDF Отчет 5
        </Button>
        <Button type="primary" onClick={() => generatePDFReport(6)} style={styles.button}>
          PDF Отчет 6
        </Button>
        <Button type="primary" onClick={() => generatePDFReport(7)} style={styles.button}>
          PDF Отчет 7
        </Button>
        <Button type="primary" onClick={() => generatePDFReport(8)} style={styles.button}>
          PDF Отчет 8
        </Button>
        <Button type="primary" onClick={() => generatePDFReport(9)} style={styles.button}>
          PDF Отчет 9
        </Button>
        <Button type="primary" onClick={() => generatePDFReport(10)} style={styles.button}>
          PDF Отчет 10
        </Button>
        <Button type="primary" onClick={() => generatePDFReport(11)} style={styles.button}>
          PDF Отчет 11
        </Button>
        <Button type="primary" onClick={() => generatePDFReport(12)} style={styles.button}>
          PDF Отчет 12
        </Button>
        <Button type="primary" onClick={() => generatePDFReport(13)} style={styles.button}>
          PDF Отчет 13
        </Button>
        <Button type="primary" onClick={() => generatePDFReport(14)} style={styles.button}>
          PDF Отчет 14
        </Button>
        <Button type="primary" onClick={() => generatePDFReport(15)} style={styles.button}>
          PDF Отчет 15
        </Button>
        <Button type="primary" onClick={() => generateExcelReport(1)} style={styles.button}>
          Excel Отчет 1
        </Button>
        <Button type="primary" onClick={() => generateExcelReport(2)} style={styles.button}>
          Excel Отчет 2
        </Button>
        <Button type="primary" onClick={() => generateExcelReport(3)} style={styles.button}>
          Excel Отчет 3
        </Button>
        <Button type="primary" onClick={() => generateExcelReport(4)} style={styles.button}>
          Excel Отчет 4
        </Button>
        <Button type="primary" onClick={() => generateExcelReport(5)} style={styles.button}>
          Excel Отчет 5
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
    fontSize: '24px',
    marginBottom: '20px',
  },
  buttonContainer: {
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