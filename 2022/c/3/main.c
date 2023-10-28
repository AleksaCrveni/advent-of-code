#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include "../libs/hashset.h"

int main() {
  hashset_t *hash_set = init();
  printf("%d", is_empty(hash_set));
  return 0;
}


