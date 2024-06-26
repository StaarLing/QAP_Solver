# QAP Solver

Этот репозиторий содержит реализацию программы для квадратичной задачи о назначениях (QAP). Решатель включает в себя различные алгоритмы для решения QAP. Приложение предоставляет удобный интерфейс для ввода задач, выбора алгоритмов, настройки параметров и визуализации результатов.

## Оглавление

- [Описание](#описание)
- [Установка](#установка)
- [Использование](#использование)
- [Алгоритмы](#алгоритмы)
  - [Алгоритм серых волков](#алгоритм-серых-волков)
  - [Алгоритм искусственной пчелиной колонии](#алгоритм-искусственной-пчелиной-колонии)
  - [Алгоритм кукушки](#алгоритм-кукушки)
  - [Алгоритм муравьиного льва](#алгоритм-муравьиного-льва)
  - [Алгоритм роя ласточек](#алгоритм-роя-ласточек)
  - [Алгоритм инвазивных сорняков](#алгоритм-инвазивных-сорняков)
  - [Алгоритм муравьиной колонии](#алгоритм-муравьиной-колонии)
  - [Алгоритм бактериального поиска пищи](#алгоритм-бактериального-поиска-пищи)
  - [Алгоритм светлячков](#алгоритм-светлячков)
  - [Алгоритм куриной стаи](#алгоритм-куриной-стаи)
- [Вклад](#вклад)

## Возможности

- **Множество алгоритмов**: Включает реализации 10 алгоритмов вдохновленных природой + решение полным перебором (для нахождения точного решения).
- **Пользовательский интерфейс**: Графический интерфейс для взаимодействия с программой, включая опции для загрузки задач, настройки параметров и просмотра результатов.
- **Визуализация**: Графическое представление задачи и процесса решения.

## Установка

1. **Клонируйте репозиторий:**

    ```
    bash
    git clone https://github.com/StaarLing/QAP_Solver.git
    cd QAP_Solver
    ```

2. **Соберите решение:**
    - Откройте файл решения (`QAP_Solver.sln`) в Visual Studio.
    - Восстановите пакеты NuGet.
    - Постройте решение.

## Использование

1. **Запустите приложение:**
    - Запустите приложение из Visual Studio, нажав `F5` или выбрав `Debug > Start Debugging`.

2. **Загрузите задачу:**
    - Используйте предоставленный интерфейс для загрузки задачи QAP из файла или создайте новую.

3. **Выберите алгоритмы:**
    - Выберите один или несколько алгоритмов из списка доступных.

4. **Настройте параметры:**
    - Настройте параметры для выбранных алгоритмов. Интерфейс позволяет изменять параметры в реальном времени.

5. **Решите задачу:**
    - Нажмите кнопку "Решить", чтобы начать процесс решения. Приложение отобразит прогресс и результаты.

6. **Просмотрите результаты:**
    - Результаты могут быть сохранены в файл или визуализированы непосредственно в приложении.

## Алгоритмы

### Алгоритм серых волков

Алгоритм серых волков имитирует поведение охоты стаи серых волков. Этот алгоритм оптимизации использует три основных типа агентов: альфа, бета и дельта, которые помогают вести стаю к лучшему решению.

### Алгоритм искусственной пчелиной колонии

Алгоритм искусственной пчелиной колонии основан на поведении пчел в поисках пищи. Пчелы-разведчики и пчелы-собиратели работают совместно для нахождения оптимальных решений.

### Алгоритм кукушки

Алгоритм кукушки использует механизм кладки яиц кукушками в гнезда других птиц. Этот метод оптимизации основан на использовании случайного поиска и замене худших решений лучшими.

### Алгоритм муравьиного льва

Алгоритм муравьиного льва имитирует охотничье поведение львов, использующих муравьев для приманивания добычи. Львы следуют за муравьями, чтобы найти наилучшие пути к решению задачи.

### Алгоритм роя ласточек

Алгоритм роя ласточек моделирует координированные полеты ласточек в поисках пищи. Ласточки используют информацию о местоположении друг друга для улучшения поиска.

### Алгоритм инвазивных сорняков

Алгоритм инвазивных сорняков основан на модели распространения и роста сорняков. Этот метод использует конкуренцию между сорняками для нахождения оптимальных решений.

### Алгоритм муравьиной колонии

Алгоритм муравьиной колонии имитирует поведение муравьев при поиске кратчайшего пути к источнику пищи. Муравьи откладывают феромоны, помогая другим муравьям следовать за ними к лучшим решениям.

### Алгоритм бактериального поиска пищи

Алгоритм бактериального поиска пищи моделирует поведение бактерий при движении к источнику пищи. Бактерии перемещаются и взаимодействуют друг с другом для нахождения оптимальных решений.

### Алгоритм светлячков

Алгоритм светлячков основан на поведении светлячков, привлекаемых светом друг друга. Светлячки используют интенсивность света для нахождения лучших решений.

### Алгоритм куриной стаи

Алгоритм куриной стаи имитирует поведение кур при поиске пищи. Куры взаимодействуют друг с другом, следуя за лидером стаи и улучшая поиск.

## Вклад

Приветствуются любые вклады! Пожалуйста, не стесняйтесь отправлять pull request или открывать issue для обсуждения улучшений или сообщения об ошибках.

## Контакты

Если у вас есть вопросы или предложения, пожалуйста, откройте issue или свяжитесь с владельцем репозитория.
email: puhov665@gmail.com