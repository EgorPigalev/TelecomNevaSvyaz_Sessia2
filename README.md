# Информационная система для компании ООО «Телеком Нева Связь»
Данный проект содержит второй блок заданий компании ООО «Телеком Нева Связь», а именно следующие данные:
+ модуль работы с абонентами;
+ модуль CRM.

## Начало работы
Для непосредственного начала работы следует скачать архивную копию данного проекта. Это можно совершить двумя способами:
Для первого способа необходимо проделать следующие действия:
+ Открыть репозиторий на сайте GitHub;
+ Нажать на кнопку "Code";
+ Выбрать пункт "Download ZIP".

Для второго способа необходимо:
+ Открыть программу "Visual Studio";
+ Нажать "Клонирование репозитория";
+ Ввести расположение репозитория и нажать на кнопку "Клонировать".

### Необходимые условия
Для установики данного программного обеспечения необходимо наличие программы ***Microsoft Visual Studio***.
Для работы с данной прогораммой необходимо наличие компьютера, удовлетовряющего минимальным требования среды разработки "Visual Studio", ознакомится с которыми можно на официальном сайте.

### Установка
Для установки данного программного обеспечения в случае выполнения первого способа в разделе "Начало работы", необходимо проделать следующие действия:
1. Взять скаченую архивную копию программы;
2. Разархивировать архив:
   + нажать ПКМ на архив;
   + выбрать нужный архиватор;
   + нажать распаковать в папку.
3. Запустить проект спомощью программы ***Microsoft Visual Studio***.

## Основные механизмы
Данный программный продукт имеет клиент-серверную архитектуру. База данных рассположена на сервере, а сама программа находится на клиентских компьютерах. В данном репозитории хранится два разработанных модуля:
1) Модуль работы с абонентами. Данный блок предназначен для просмотра списка клиентов организации. Данный список можно фильтровать по актуальности воспользавшись нижними пунктами. Кроме этого возможен поиск абонентов поиск абонентов по фамилии, району, улице, лицевому счету. При поиске абонентов по улице реализован выбор улицы с номером дома с помощью выпадающего списка. При двойном нажатие на абонента открывается страница с подробным описанием.
2) Модуль "CRM". Данный блок предназначен для формирования заявки позвонившего абонента. Кроме создания заявки возможна реализация тестирования оборудования.
Кроме этого слева реализовано меню, которое имеет два формата:
+ развернутый вид, когда меню содержит текстовые и графические пункты управления;
+ свернутый вид, когда все пункты меню будут представлены пиктограммами.

Для выбора авторизованного сотрудника сделан выпадающий список со всеми пользователями из БД. При выборе пользователя меняется его фотография. 

## Авторы
Люди которые принимали участие в разработке проекта:
* **Пигалев Егор** - [PigalevEgor](https://github.com/EgorPigalev)
