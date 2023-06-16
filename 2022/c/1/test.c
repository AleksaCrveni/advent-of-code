#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <string.h>
void func(int *arr, int newValue) {
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

int main() {
    int index = 1;
    int newValue = 0;
    int arr[3] = {1,3,4};
    func(arr, newValue);
    for (int i =0; i<3;i ++) {
        printf("%d\n", arr[i]);
    }
}


