# SenseCapitalTask
## Описание проекта
База данных реализованна при помощи Entity Framework, миграции бд содержаться в папке Migrations.
Для отправки запросов использовался Swagger
## Описание API
### UserController - работа с аккаунтами
1. `/api/User/Register` - метод регистрации нового пользователя(пароль должен содержать минимум 5 символов)
2. `/api/User/Login` - метод входа в аккаунт
3. `/api/User/Logout` - метод выхода из аккаунта
### InvitationController - работа с приглашениями (доступен только зарегистрированным пользователям)
1. `/api/Invitation` - просмотр приглашений в игру
2. `/api/Invitation/Send` - отправка приглашения в игру по имени пользователя
3. `/api/Invitation/Accept` - принятие приглашения по id
4. `/api/Invitation/Cancel` - отклонение приглашения по id
### GameController - работа с игровыми механиками (доступен только зарегистрированным пользователям)
1. `/api/Game` (Get) - просмотр активной игры
2. `/api/Game` (Delete) - завершение/удаление активной игры
3. `/api/Game/MakeTurn` - игровой ход(порядок хода и фигура, которой ходит игрок, определяется автоматически)

___
Игровое поле представляет собой строку `"*********"`, игроки могут указать позицию куда они хотят походить (0 - 8).
Совершить два хода подряд невозможно, также нельзя сходить в одно и тоже место дважды(ни одному игроку, ни двум по очереди)

