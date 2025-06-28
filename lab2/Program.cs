using System;

public class Payment
{
    // Поля класса
    public string FullName;
    public decimal Salary;
    public int HireYear;
    public decimal BonusPercent;
    public int WorkedDays;
    public int WorkingDays;

    // Свойства
    public decimal AccruedAmount => CalculateAccrued();
    public decimal WithheldAmount => CalculateWithheld();
    public decimal NetAmount => CalculateNet();
    public int Experience => CalculateExperience();

    // Конструкторы (перегрузка)
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

    // Перегрузка конструктора - без процента надбавки
    public Payment(string fullName, decimal salary, int hireYear,
                  int workedDays, int workingDays)
        : this(fullName, salary, hireYear, 0m, workedDays, workingDays) {}

    // Методы расчета
    private decimal CalculateAccrued()
    {
        decimal baseAmount = Salary * WorkedDays / WorkingDays;
        decimal bonus = baseAmount * BonusPercent / 100;
        return baseAmount + bonus;
    }

    private decimal CalculateWithheld()
    {
        decimal pensionFund = AccruedAmount * 0.01m;
        decimal incomeTax = (AccruedAmount - pensionFund) * 0.13m;
        return pensionFund + incomeTax;
    }

    private decimal CalculateNet() => AccruedAmount - WithheldAmount;
    private int CalculateExperience() => DateTime.Now.Year - HireYear;

    // Перегрузка метода вывода инфы
    public void PrintPaymentInfo()
    {
        Console.WriteLine($"ФИО: {FullName}");
        Console.WriteLine($"Стаж: {Experience} лет");
        Console.WriteLine($"Начислено: {AccruedAmount:C}");
        Console.WriteLine($"Удержано: {WithheldAmount:C}");
        Console.WriteLine($"К выплате: {NetAmount:C}");
    }

    // Перегрузка метода - краткая версия вывода
    public void PrintPaymentInfo(bool shortVersion)
    {
        if (shortVersion)
        {
            Console.WriteLine($"{FullName}: {NetAmount:C} (на руки)");
        }
        else
        {
            PrintPaymentInfo(); // Вызов полной версии
        }
    }
}

class Program
{
    static void Main()
    {
        // Использование перегруженных конструкторов
        Payment emp1 = new Payment("Иванов И.И.", 50000m, 2015, 15m, 20, 22);
        Payment emp2 = new Payment("Петров П.П.", 60000m, 2018, 20, 22);

        // Использование перегруженных методов
        emp1.PrintPaymentInfo(); // Полная версия
        emp2.PrintPaymentInfo(true); // Краткая версия
    }
}
