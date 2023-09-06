# WebApiNetCoreTemplate (.Net 6)

MicroserviceTemplate - шаблонный проект для создания REST API сервиса с уже преднастроенной структурой,
включая логирование, DI, AutoMapper, Exception MiddleWare, RequestFiltering и т.д.
В результате на выходе будет .vsix расширение с подготовленным шаблоном,
которое после установки в Visual Studio позволит создавать пустой проект стандартными
средствами File -> New Project -> MicroserviceTemplate.

Решение состоит из двух проектов:
- MicroserviceTemplate
Собственно проект, который будет использоваться для создания шаблона. После обновления\изменения этого
проекта необходимо экспортировать проект как шаблон Project -> Export Template.
Далее в мастере экспорта необходимо при необходимости заполнить доп. поля, после чего создастся шаблон по пути 
%USERPROFILE%\Documents\Visual Studio 2022\My Exported Templates\MicroserviceTemplate.zip
- MicroserviceTemplateVSIX
Проект для создания файла расширения .vsix
После создания шаблона проекта в предыдущем пункте необходимо zip архив подложить в папку ProjectTemplates.
Изменить версию сборки в файле source.extension.vsixmanifest и сбилдить проект.
После билда в папку проекта VSIXContent будет скопирован файл расширения MicroserviceTemplateVSIX.vsix
