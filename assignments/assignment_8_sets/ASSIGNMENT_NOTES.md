Assignment 8: Spell Checker and Vocabulary Explorer

Name: Vitoria Resende

HashSet Pattern Understanding

I learned that HashSet is great because it checks things really fast. When I use Contains it feels instant. It also keeps only unique items automatically so I don’t need to worry about duplicates. This helped a lot when I was separating correct and misspelled words because I only needed to check each word once.

Challenges and Solutions

My biggest challenge was making the file load correctly and understanding how normalization works. At first my dictionary was not loading and I thought the code was wrong, but it was just the file path.
I solved it by learning how to clean the words the same way every time so the lookups match. Also understanding where Visual Studio runs the program helped me fix the file issue.

The most confusing part was learning all the small details about text cleaning like punctuation, spaces, and lowercase. Once I used NormalizeWord for everything it became much easier.

Code Quality

I am most proud of my normalization method because it made the whole assignment work smoothly. I also like that my code is clean and simple to read.

If I had more time I would improve the tokenization so it handles more complex punctuation and maybe handle numbers differently.

Testing Approach

I tested by loading the dictionary first and then analyzing the sample text files included in the project.
I also used the menu to check misspelled words, unique words, and individual words.

Some scenarios I tested were
words with uppercase and lowercase
words with punctuation like hello. or world
empty inputs
fake words that are not in the dictionary

I found an issue with the dictionary not loading and I fixed it by moving the file to the correct folder.

HashSet vs List Understanding

I learned that I should use HashSet when I need fast lookups and only unique items.
List is better when I need to keep order or allow duplicates, like when I want to count how many times a word appears in the text.

The performance difference was very obvious because HashSet checking is instant.

Real-World Applications

This relates to real spell checkers because they also need fast lookups and good normalization. They most likely use some type of set or dictionary structure similar to this.
I also learned that real text has many strange characters and punctuation so normalization is very important.

Stretch Features

None implemented yet

Time Spent

Total time: 4 hours

Understanding requirements and HashSet ideas: about 1 hour
Implementing the core methods: about 2 hours
Testing different files: around 30 minutes
Debugging file loading: around 20 minutes
Writing these notes: around 10 minutes

The most time-consuming part was getting normalization right and making sure the dictionary loaded correctly.

Key Learning Outcomes

I learned that HashSet gives O(1) lookup, automatic uniqueness, and makes categorizing very easy.
I also learned how important normalization is whenever we work with real text because the same word can appear in many forms.
For software engineering, I learned about defensive programming, checking for errors, handling files, and making the program more user friendly.