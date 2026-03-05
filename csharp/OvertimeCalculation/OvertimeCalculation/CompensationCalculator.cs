namespace OvertimeCalculation;

public class CompensationCalculator
{
    public const decimal MaxOvertimeHoursRate1 = 10m;
    public const int ThresholdOvertimeHoursRate2 = 6;

    public static Overtime CalculateOvertime(decimal hoursOvertimeTotal, Assignment assignment, Briefing briefing)
    {
        decimal hoursOvertimeRate1 = 0m;
        decimal hoursOvertimeRate2 = 0m;

        bool isWatcodeUnion = briefing.Watcode && assignment.IsUnionized;
        bool isWatcodeNonUnionForeign = briefing.Watcode && !assignment.IsUnionized && briefing.Foreign;

        if (
            (!briefing.Watcode && !briefing.Z3 && !assignment.IsUnionized)
            || (briefing.Hbmo && assignment.IsUnionized)
            || isWatcodeNonUnionForeign
            || isWatcodeUnion
            || (briefing.Foreign && !assignment.IsUnionized)
        )
        {
            hoursOvertimeRate1 = hoursOvertimeTotal;
        }
        else
        {
            if (hoursOvertimeTotal <= 0m)
            {
                return new Overtime(hoursOvertimeRate1, hoursOvertimeRate2);
            }
            else if (hoursOvertimeTotal <= MaxOvertimeHoursRate1)
            {
                hoursOvertimeRate1 = hoursOvertimeTotal;
            }
            else
            {
                hoursOvertimeRate1 = MaxOvertimeHoursRate1;
                hoursOvertimeRate2 = hoursOvertimeTotal - MaxOvertimeHoursRate1;
                if (assignment.IsUnionized)
                {
                    decimal threshold = CalculateThreshold(assignment, ThresholdOvertimeHoursRate2);
                    hoursOvertimeRate2 = Math.Min(hoursOvertimeRate2, threshold);
                }
            }
        }

        return new Overtime(hoursOvertimeRate1, hoursOvertimeRate2);
    }

    private static decimal CalculateThreshold(Assignment listEntry, long threshold)
    {
        TimeSpan remainder = listEntry.Duration - TimeSpan.FromHours(threshold);
        if (remainder < TimeSpan.Zero)
        {
            return (decimal)listEntry.Duration.TotalHours;
        }

        return (decimal)threshold;
    }
}
