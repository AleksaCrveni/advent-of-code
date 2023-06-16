#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <string.h>

/*
    Author Aleksa Crveni 2023
    Time: O(n)
    Space: O(n) ??
*/

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
    while((c = fgetc(f)) != EOF) {
        if (c == '\n') {
            c = fgetc(f);
            sum += atoi(numArr);
            if (c == '\n' || c == EOF) {
                if (sum > max)
                    max = sum;
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
    if (sum > max)
        max = sum;

    printf("%d", max);
    fclose(f);
}

