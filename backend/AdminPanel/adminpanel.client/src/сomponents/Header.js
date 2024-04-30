import React from 'react';

function Header() {
  return (
    <div className="header">
      <div className="tab">Заявки</div>
      <div className="tab">Сотрудники</div>
      <div className="tab">Инциденты</div>
      <div className="tab">Публикации</div>
      <div className="user-info">Информация о текущем сотруднике</div>
    </div>
  );
}

export default Header;