#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <string.h>

/*
	Author Aleksa Crveni 2023
	Time: O(n)

    Change : 
    The Elf finishes helping with the tent and sneaks back over to you.
    "Anyway, the second column says how the round needs to end: X means you need to lose,
    Y means you need to end the round in a draw, and Z means you need to win. Good luck!"

	A, X -> Rock
	B, Y -> Paper
	C, Z -> Scissors
    
    X -> LOST
    Y -> DRAW
    Z -> WIN

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
				if (p1 == 'A')
					totalSum += 3;
				else if (p1 == 'B')
					totalSum += 1;
				else if (p1 == 'C')
					totalSum += 2;
			} else if (p2 == 'Y') {
				totalSum += 3;
				if (p1 == 'A')
					totalSum += 1;
				else if (p1 == 'B')
					totalSum += 2;
				else if (p1 == 'C')
					totalSum += 3;
			} else if (p2 == 'Z') {
				totalSum += 6;
				if (p1 == 'A')
					totalSum += 2;
				else if (p1 == 'B')
					totalSum += 3;
				else if (p1 == 'C')
					totalSum += 1;
			} else {
				printf("NOT MATCH");
			}
		}

		printf("%d", totalSum);
}