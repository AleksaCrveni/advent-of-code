#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <string.h>



/*
	Author Aleksa Crveni 2023
	Time: O(n) + O(3)

	A, X -> Rock
	B, Y -> Paper
	C, Z -> Scissors

	Paper > Rock
	Scizzors > Paper
	Rock > Scizzors

	Points:
 
	ROCK  	 = 1;
	PAPER    = 2;
	SCISSORS = 3;

	LOST = 0;
	DRAW = 3;
	WON  = 6;

*/

void updateTop3(int *arr, int newValue);
int main()
{
    FILE *f;
    bool dummy = false;
    char input[5];

		int dummyExpectedRes = 15;
    if (dummy)
			f = fopen("dummyInput.txt", "r");
    else
			f = fopen("input.txt", "r");

    if (f == NULL) {
			printf("Not able to open the file!");
    }

		char p1 = '\0';
		char p2 = '\0';
		int totalSum = 0;
    while (fgets(input, 5, f) != NULL) {
			p1 = input[0];
			p2 = input[2];

			if (p2 == 'X') {
				totalSum += 1;
				if (p1 == 'A')
					totalSum += 3;
				else if (p1 == 'C')
					totalSum += 6;
				else if (p1 != 'B')
					printf("NOT MATCH X");
			} else if (p2 == 'Y') {
				totalSum += 2;
				if (p1 == 'B')
					totalSum += 3;
				else if (p1 == 'A')
					totalSum += 6;
				else if (p1 != 'C')
					printf("NOT MATCH B");
			} else if (p2 == 'Z') {
				totalSum += 3;
				if (p1 == 'C')
					totalSum += 3;
				else if (p1 == 'B')
					totalSum += 6;
				else if (p1 != 'A')
					printf("NOT MATCH C");
			} else {
				printf("NOT MATCH");
			}
		}
		// 14375
		printf("%s", input);
		printf("%d", totalSum);
}