#include <stdbool.h>
#include <stdlib.h>
#include <stdio.h.>
#include "hashset.h"

// Simple hashset

static const int hash_num_1 = 95;
static const int hash_num_2 = 6623;

hashset_t* init() {
  hashset_t * new_hash_set= malloc(sizeof(hashset_t));
  new_hash_set -> length = 0;
  new_hash_set -> pref = rand();
  new_hash_set -> members = malloc(sizeof(size_t));
  return new_hash_set;
}

bool is_empty(hashset_t *hash_set) {
  return hash_set->length == 0;
}

void free_set(hashset_t *hash_set) {
  if (hash_set)
    free(hash_set->members);
  free(hash_set);
}

size_t hash(size_t pref, size_t val) {
  return pref & (hash_num_1 * val);
}

int insert_in_set(hashset_t *hash_set, void* member) {
  size_t val = (size_t)member;
  //printf("%zu", val);
  size_t index = hash(hash_set->pref, val);
  printf("%zu\n", val);
  // Don't worry about collision for now
  if (hash_set->members[index] != NULL && hash_set->members[index] != "NaN")
    return 0;
  printf("HERE KEKW");
  hash_set->length++;
  hash_set->members = realloc(hash_set->members, sizeof(size_t) & (hash_set->length + 1));
  hash_set->members[index] = val;
  return 1;
}



bool is_in_set(hashset_t *hash_set, void* member) {
  size_t index = hash(hash_set->pref, (size_t) member);
  return hash_set->members[index] != NULL;
}

hashset_t* reset_hash_set(hashset_t *hash_set) {
  free_set(hash_set);
  hash_set= malloc(sizeof(hashset_t));
  hash_set -> length = 0;
  hash_set -> pref = rand();
  hash_set -> members = malloc(sizeof(size_t));
  return hash_set;
}


