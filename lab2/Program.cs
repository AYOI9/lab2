using System;

public class Payment
{
    // Поля класса
    public string FullName;       // ФИО
    public decimal Salary;        // Оклад
    public int HireYear;          // Год поступления на работу
    public decimal BonusPercent;  // Процент надбавки
    public int WorkedDays;        // Отработанные дни
    public int WorkingDays;       // Рабочие дни в месяце

    // Вычисляемые свойства
    public decimal AccruedAmount => CalculateAccrued();
    public decimal WithheldAmount => CalculateWithheld();
    public decimal NetAmount => CalculateNet();
    public int Experience => CalculateExperience();

    // Конструктор
    public Payment(string fullName, decimal salary, int hireYear,
                  decimal bonusPercent, int workedDays, int workingDays)
    {
        FullName = fullName;
        Salary = salary;
        HireYear = hireYear;
        BonusPercent = bonusPercent;
        WorkedDays = workedDays;
        WorkingDays = workingDays;
    }

    // Метод вычисления начисленной суммы
    private decimal CalculateAccrued()
    {
        decimal baseAmount = Salary * WorkedDays / WorkingDays;
        decimal bonus = baseAmount * BonusPercent / 100;
        return baseAmount + bonus;
    }

    // Метод вычисления удержанной суммы
    private decimal CalculateWithheld()
    {
        decimal pensionFund = AccruedAmount * 0.01m; // 1% в пенсионный фонд
        decimal incomeTax = (AccruedAmount - pensionFund) * 0.13m; // 13% подоходный
        return pensionFund + incomeTax;
    }

    // Метод вычисления суммы "на руки"
    private decimal CalculateNet()
    {
        return AccruedAmount - WithheldAmount;
    }

    // Метод вычисления стажа
    private int CalculateExperience()
    {
        int currentYear = DateTime.Now.Year;
        return currentYear - HireYear;
    }

    // Метод для вывода информации
    public void PrintPaymentInfo()
    {
        Console.WriteLine($"ФИО: {FullName}");
        Console.WriteLine($"Стаж: {Experience} лет");
        Console.WriteLine($"Начислено: {AccruedAmount:C}");
        Console.WriteLine($"Удержано: {WithheldAmount:C}");
        Console.WriteLine($"К выплате: {NetAmount:C}");
    }
}
        // Создаем объект Payment
        Payment employee = new Payment(
            "Иванов Иван Иванович",
            50000m,
            2015,
            15m,
            20,
            22);

        // Выводим информацию о зарплате
        employee.PrintPaymentInfo();
