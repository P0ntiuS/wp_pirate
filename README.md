## Установка Unity

1) [Установить UnityHub](https://unity3d.com/get-unity/download). Нажать на кнопку "Download Unity Hub".
2) Запустить UnityHub, перейти во вкладку Installs, если она уже активна, нажать на кнопку ADD
3) Выбрать Unity Engine 2019.4.*. нажать Next
4) Убираем все галочки, если у вас не установлена Microsoft Visual Studio 2019,
ставим галочку на против MVS Community Edition 2019. Нажимаем Done.
* Примечание: для работы с кодом Unity можно использовать и другие редакторы, например Visual Studio Code, Rider.
Мы будем использовать Microsoft Visual Studio 2019, вы можете пользоваться каким вам удобно редактором.

## Добавление компонентов в Microsoft Visual Studio 2019 для Unity

1) Открыть Visual Studio Installer
2) Нажать на кнопку Изменить напротив своей версии MVS 2019
3) В разделе игры, выбрать компонент разработка игр с помощью Unity
4) Нажать изменить

## Добавление проекта WP_Pirate

1) Клонируйте себе репозиторий
2) Открыть Unity HUB и перейти во вкладку Projects
3) Нажмите ADD и добавьте папку WP_Pirate
4) Проект был создан на Unity версии 2019.4.10f1, 
если у вас будет версия выше, например 2019.4.11f1. 
Кликните на выпадающий список в колонке Unity Version и выберите свою версию.
Также при открытии проекта нажмите на кнопку Confirm.
5) Нажмите на строчку с проектом.
6) После открытия в редакторе Unity, снизу вы увидите файловый менеджер
7) Зайдем в папку Scirpts и откроем скрипт Hello, для проверки редактора
8) Если у вас открылся не тот редактор зайдем в Edit->Preferences->External Tools->External Script Editor
9) В выпадающем списке выберите нужный редактор, если его нет нажмите на Browse
10) Для MVS 2019 Microsoft Visual Studio\2019\Community\Common7\IDE\devenv.exe.
Для проверки снова откройте скрипт Hello

По любым вопросам писать <Maksim_Kabanov@epam.com>