<script setup>
import { useToggle } from '@vueuse/shared'
import { useDark } from "@vueuse/core";

const isDark = useDark()
const toggleDark = useToggle(isDark)
</script>

<template>
  <button @click="toggleDark()">
    <span class="ml-2">{{ isDark ? 'Dark' : 'Light' }}</span>
  </button>
</template>