typedef struct {
  size_t *members;
  size_t length;
  size_t pref;
} hashset_t;

hashset_t* init();
bool is_empty(hashset_t *hash_set);
int insert_in_set(hashset_t *hash_set, void* member);
void free_set (hashset_t *hash_set);
bool is_in_set(hashset_t *hash_set, void* member);