mergeInto(LibraryManager.library, {
    // Имя функции, которую мы будем вызывать из C#
    OpenURLInExternalWindow: function (urlPtr) {
        // Преобразуем строку из C# в строку JavaScript
        var url = Pointer_stringify(urlPtr);
        // Открываем URL в новой вкладке
        window.open(url, "_blank");
    }
});