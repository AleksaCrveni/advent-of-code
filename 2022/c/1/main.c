#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <string.h>

/*
    Author Aleksa Crveni 2023
    Time: O(n)
    Space: O(n) ??
*/

void updateTop3(int *arr, int newValue);
int main()
{
    FILE *f;
    bool dummy = false;
    if (dummy)
        f = fopen("dummyInput.txt", "r");
    else
        f = fopen("input.txt", "r");

    if (f == NULL) {
        printf("Not able to open the file!");
    }
    char c;
    char *numArr = malloc(sizeof(char) * 6);
    memset(numArr, 0, 6);
    int sum = 0;
    int max = -1;
    int i = 0;
    int top3[3] = {-3,-2,-1};
    while((c = fgetc(f)) != EOF) {
        if (c == '\n') {
            c = fgetc(f);
            sum += atoi(numArr);
            if (c == '\n' || c == EOF) {
                updateTop3(top3, sum);
                sum = 0;
                memset(numArr, 0, 6);
                i =0;
            } else {
                memset(numArr, 0, 6);
                i =0;
                numArr[i++] = c;
            }
            
        } else {
            numArr[i++] = c;
        }
    }

    sum += atoi(numArr);
   	updateTop3(top3, sum);

    printf("TOP 1: %d calories.\n", top3[2]);
    int totalSum = 0;
		for (int i =0;i < 3;i++) {
			totalSum += top3[i];
		}
		printf("TOP 3 SUM: %d calories.\n", totalSum);
    fclose(f);
}

void updateTop3(int *arr, int newValue) {
		if (arr[0] < newValue)
			arr[0] = newValue;
		for (int i = 1; i <= 2; i++) {
			if (arr[i] < newValue) {
				arr[i - 1] = arr[i];
				arr[i] = newValue;
			} else
				i = 3;
		}
}

