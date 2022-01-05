#include <gtest/gtest.h>

extern "C"
{
#include "sample.h"
}

using namespace std;

TEST(SampleSuite, SampleTest) {
    ASSERT_EQ(42, 1);
}