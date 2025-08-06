<template>
<a-config-provider ref="configProvider" :locale="locale">
  <div id="app">
    <router-view/>
  </div>
</a-config-provider>
<!--  <a-locale-provider :locale="locale">-->
<!--    -->
<!--  </a-locale-provider>-->
</template>
<script>
  import zhCN from 'ant-design-vue/lib/locale-provider/zh_CN'
  import jaJP from 'ant-design-vue/lib/locale-provider/ja_JP'
  import enUS from 'ant-design-vue/lib/locale-provider/en_US'
  import enquireScreen from '@/utils/device'
  import moment from 'moment';
  import 'moment/locale/zh-cn';
  import 'moment/locale/ja';
  import 'moment/locale/en-gb';
  moment.locale('zh-cn');
  export default {
    data () {
      return {
        locale: {},
        lang:"",
      }
    },
    watch:{
      '$store.state.app.lang'(newVal){
          if(newVal !== this.lang){
            this.lang = newVal;
            this.setLanguage(newVal);
          }
      }
    },
/*    beforeCreate() {
      console.log("beforeCreate")
      this.lang = this.$ls.get("language", "zh-CN");
      this.setLanguage(this.lang);
    },*/
    mounted() {
      console.log("mounted")
      this.setLanguage(this.$ls.get("language", "ja-JP"));

    },
    created () {
      //加载本地语言或者初始化语言
      this.$store.commit('initLanguage');
      let that = this
      enquireScreen(deviceType => {
        // tablet
        if (deviceType === 0) {
          that.$store.commit('TOGGLE_DEVICE', 'mobile')
          that.$store.dispatch('setSidebar', false)
        }
        // mobile
        else if (deviceType === 1) {
          that.$store.commit('TOGGLE_DEVICE', 'mobile')
          that.$store.dispatch('setSidebar', false)
        }
        else {
          that.$store.commit('TOGGLE_DEVICE', 'desktop')
          that.$store.dispatch('setSidebar', true)
        }

      })
    },
    methods:{
      setLanguage(lang) {
        switch (lang) {
          case "ja-JP":
            this.locale = jaJP;
            break;
          case "en-US":
            this.locale = enUS;
            break;
          default:
            this.locale = zhCN;
            break;
        }
        //this.$forceUpdate();
      },
    }
  }
</script>
<style>
  #app {
    height: 100%;
  }
</style>