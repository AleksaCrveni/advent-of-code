
use std::fs::File;
use std::io::BufReader;

fn main() {
    let file_path: &'static str = "./testInput.txt";
    let f = File::open(file_path)?;

    let mut buf = String::new();
    let mut reader = BufReader::new(f);
}
