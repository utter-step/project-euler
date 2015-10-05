from functools import wraps
from math import sqrt
from time import time


words = open('p098_words.txt').read().split(',')
words = [word.strip('"') for word in words]


def benchmark(f):
    TIMES = 500

    @wraps(f)
    def wrapped(*args, **kwargs):
        start = time()

        for i in xrange(TIMES):
            result = f(*args, **kwargs)

        span = time() - start

        args_str = map(str, args)
        kwargs_str = map(
            lambda k, v: "{0}={1}".format(k, v),
            kwargs.iteritems()
        )
        params_str = ", ".join(args_str + kwargs_str)
        if len(params_str) > 30:
            params_str = params_str[:27] + "..."

        print "{0}({1}) == {2}".format(
            f.__name__,
            params_str,
            result,
        )
        print "avg time: {0:.5f} ms.".format(span / TIMES * 1000)

        return result
    return wrapped


def group_by_anagrams(words):
    anagrams = {}
    for word in words:
        key = "".join(sorted(word))
        if key in anagrams:
            anagrams[key].append(word)
        else:
            anagrams[key] = [word]

    return {
        key: words for key, words in anagrams.iteritems()
        if len(words) > 1
    }


def is_square(num):
    root = sqrt(num)
    return root == int(root)


def num_to_mask(num, word):
    mask = {}

    for i in xrange(len(num)):
        if word[i] in mask:
            return
        mask[word[i]] = num[i]

    return mask


def apply_mask(mask, word):
    res = []

    for c in word:
        res.append(mask[c])

    return "".join(res)


@benchmark
def solve(words):
    anagrams = group_by_anagrams(words)
    anagrams_by_len = {}
    unique_count = {}

    for key, words in anagrams.iteritems():
        unique_count[key] = len(set(key))
        if len(key) in anagrams_by_len:
            anagrams_by_len[len(key)][key] = words
        else:
            anagrams_by_len[len(key)] = {key: words}

    max_length = max(anagrams_by_len.keys())
    max_found = 0
    max_found_length = 0

    for candidate in xrange(int(sqrt(10 ** max_length)) + 1, 96, -1):
        candidate = candidate * candidate
        c_str = str(candidate)
        c_str_set = set(c_str)

        if len(c_str) < max_found_length:
            return max_found

        if len(c_str) not in anagrams_by_len:
            continue

        anagrams_i = anagrams_by_len[len(c_str)]

        for key, words in anagrams_i.iteritems():
            if len(c_str_set) == unique_count[key]:
                mask = num_to_mask(c_str, words[0])
                if not mask:
                    continue
                for word in words[1:]:
                    num = apply_mask(mask, word)
                    if num[0] != '0' and is_square(int(num)):
                        max_found = max(candidate, max_found, int(num))
                        max_found_length = len(num)

    return max_found


solve(words)
