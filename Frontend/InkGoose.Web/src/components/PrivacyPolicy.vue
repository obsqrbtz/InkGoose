<script setup>
import Navbar from './Navbar.vue';
import Footer from './Footer.vue'

//var content = this.privacyPolicyHtml;
//console.log("Privacy policy: " + this.privacyPolicyHtml)

</script>

<template>
    <Navbar />
    <div class="grid w-full justify-center">
        <div
            class="prose prose-md mb-4"
            v-html="content"
        />
    </div>
    <Footer />
</template>

<script>
export default {
    data() {
        return {
            content: this.privacyPolicyHtml
        }
    },
};
</script>