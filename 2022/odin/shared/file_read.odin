package shared
import "core:os"
read_file_original :: proc(filename: string) -> (data: []byte, ok: bool) {
  data, ok = os.read_entire_file(filename, context.allocator)
  if !ok do return 
  //defer delete(data)
  return
}