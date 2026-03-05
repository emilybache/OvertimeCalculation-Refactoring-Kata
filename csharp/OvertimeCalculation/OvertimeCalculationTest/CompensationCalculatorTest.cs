using ApprovalTests;
using ApprovalTests.Combinations;
using ApprovalTests.Reporters;
using OvertimeCalculation;

namespace OvertimeCalculationTest;

[UseReporter(typeof(QuietReporter))]
public class CompensationCalculatorTest
{
    [Test]
    public void CalculateOvertime()
    {
        int[] assignmentHours = { 0, 5, 6 };
        int[] overtimeTotalHours = { 0, 1, 10, 12, 17 };
        CombinationApprovals.VerifyAllCombinations(DoCalculateOvertime,
            assignmentHours,
            overtimeTotalHours,
            new[] { true, false },
            new[] { true, false },
            new[] { true, false },
            new[] { true, false },
            new[] { true, false }
        );
    }

    private Overtime DoCalculateOvertime(int assignmentHours, int overtimeTotalHours, bool isUnionized, bool watcode, bool z3, bool foreign, bool hbmo)
    {
        var assignment = new Assignment(isUnionized, TimeSpan.FromHours(assignmentHours));
        var briefing = new Briefing(watcode, z3, foreign, hbmo);
        decimal overtimeTotal = overtimeTotalHours;
        return CompensationCalculator.CalculateOvertime(overtimeTotal, assignment, briefing);
    }
}
