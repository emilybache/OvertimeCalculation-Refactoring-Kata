package codingdojo;

import org.approvaltests.combinations.CombinationApprovals;
import org.junit.jupiter.api.Test;

import java.math.BigDecimal;
import java.time.Duration;

import static org.junit.jupiter.api.Assertions.assertEquals;

public class CompensationCalculatorTest {
    @Test
    void calculateOvertime() {
        Integer[] assignmentHours = {0, 5, 6};
        Integer[] overtimeTotalHours = {0, 1, 10, 12, 17};
        CombinationApprovals.verifyAllCombinations(this::doCalculateOvertime,
                assignmentHours,
                overtimeTotalHours,
                new Boolean[] {true, false},
                new Boolean[] {true, false},
                new Boolean[] {true, false},
                new Boolean[] {true, false},
                new Boolean[] {true, false}
        );

    }

    private Overtime doCalculateOvertime(int assignmentHours, int overtimeTotalHours, boolean isUnionized, boolean watcode, boolean z3, boolean foreign, boolean hbmo) {
        Assignment assignment = new Assignment(isUnionized, Duration.ofHours(assignmentHours));
        Briefing briefing = new Briefing(watcode, z3, foreign, hbmo);
        BigDecimal overtimeTotal = BigDecimal.valueOf(overtimeTotalHours);
        return CompensationCalculator.calculateOvertime(overtimeTotal, assignment, briefing);
    }

}
