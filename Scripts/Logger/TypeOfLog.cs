namespace Common
{
/// <summary>
/// Описание классификации собираемой информации
/// </summary>
public enum TypeOfLog {
  /// <summary>
  /// Исключение
  /// </summary>
  Exception = 0,

  /// <summary>
  /// Критическая ошибка
  /// </summary>
  FatalError = 1,

  /// <summary>
  /// Информация о проекте
  /// </summary>
  ProjectInfo = 2,

  /// <summary>
  /// Информация о модуле SkeletonTracking
  /// </summary>
  SkeletonTracking = 3,

  /// <summary>
  /// Информация о 3D-моделях
  /// </summary>
  ModelInfo = 4,

  /// <summary>
  /// Информация об интерфейсе (взаимодействие, нажатые кнопки, интересы)
  /// </summary>
  GuiInfo = 5,

  /// <summary>
  /// Информация об одежде и текстурах (взаимодействие, время интереса)
  /// </summary>
  ClothesInfo = 6,

  /// <summary>
  /// Дополнительная информация о пользователе
  /// </summary>
  UserCommon = 7,

  /// <summary>
  /// Появление пользователя
  /// </summary>
  NewUser = 8,

  /// <summary>
  /// Исчезание пользователя
  /// </summary>
  LostUser = 9,

  /// <summary>
  /// Обновление пола
  /// </summary>
  Gender = 10

}  // enum
}
