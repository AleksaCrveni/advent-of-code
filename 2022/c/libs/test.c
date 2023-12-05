#include <stdio.h>
#include <stdbool.h>
#include "hashset.h"

int main() {
  hashset_t *hash_set = init();
  insert_in_set(hash_set, "test");
  printf("%d", hash_set->length);
  return 0;
}