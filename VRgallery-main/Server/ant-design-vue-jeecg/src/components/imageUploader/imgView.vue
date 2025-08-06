<template>
  <viewer :images="images">
    <div class="imgDiv" v-for="(item,index) in images" :key="'img'+index" @mouseover="mouseOver(index)"  @mouseleave="mouseLeave(index)">
      <img :src="item.src" :key="item.index" width="148" height="148" style="margin-right:10px;">
      <p class="iconStyle" v-if="item.show" @click="deleteImg(item)">
        <i class="el-icon-close"></i>
      </p>

    </div>

  </viewer>
</template>


<script>
import Vue from 'vue';
import Viewer from 'v-viewer'
import 'viewerjs/dist/viewer.css'
Vue.use(Viewer);
Viewer.setDefaults({
  'inline':false,//启用 inline 模式
  'button':true, //右上角按钮
  "navbar": true, //底部缩略图
  "title": true, //当前图片标题
  "toolbar": true, //底部工具栏
  "tooltip": true, //显示缩放百分比
  "movable": true, //是否可以移动
  "zoomable": true, //是否可以缩放
  "rotatable": true, //是否可旋转
  "scalable": true, //是否可翻转
  "transition": true, //使用 CSS3 过度
  "fullscreen": true, //播放时是否全屏
  "keyboard": true, //是否支持键盘
  "url": "data-source",
  ready: function (e) {
    console.log(e.type,'组件以初始化');
  },
  show: function (e) {
    console.log(e.type,'图片显示开始');
  },
  shown: function (e) {
    console.log(e.type,'图片显示结束');
  },
  hide: function (e) {
    console.log(e.type,'图片隐藏完成');
  },
  hidden: function (e) {
    console.log(e.type,'图片隐藏结束');
  },
  view: function (e) {
    console.log(e.type,'视图开始');
  },
  viewed: function (e) {
    console.log(e.type,'视图结束');
  },
  zoom: function (e) {
    console.log(e.type,'图片缩放开始');
  },
  zoomed: function (e) {
    console.log(e.type,'图片缩放结束');
  }
});

export default {
  props: {
    imageList:Array | Object,//预览传进来的图片地址，这里因为项目中已经定型了，所以传一个string进来，然后手动转 array
  },
  watch: {
    //传来的图片地址
    imageList(val){
      let type = Array.isArray(val);
      if(!type){
        this.changeArr(val.url);
      }else{
        this.changeArrT(val);
      }
    },
  },
  data () {
    return {
      isShow:false,//是否展示预览弹框
      images:[
        // {src:'https://ss0.bdstatic.com/70cFvHSh_Q1YnxGkpoWK1HF6hhy/it/u=3272199364,3404297250&fm=26&gp=0.jpg',index:1},
        // {src:'https://ss3.bdstatic.com/70cFv8Sh_Q1YnxGkpoWK1HF6hhy/it/u=3267295166,2381808815&fm=26&gp=0.jpg',index:2},
        // {src:'https://ss0.bdstatic.com/70cFvHSh_Q1YnxGkpoWK1HF6hhy/it/u=3429654663,2972188411&fm=26&gp=0.jpg',index:3},
        // {src:'https://ss3.bdstatic.com/70cFv8Sh_Q1YnxGkpoWK1HF6hhy/it/u=3597323878,2962972725&fm=26&gp=0.jpg',index:4},
      ]
    };
  },

  components: {},

  computed: {},

  created() {},

  mounted() {
    let type = Array.isArray(this.imageList);
    if(!type){
      this.changeArr(this.imageList.url);
    }else{
      this.changeArrT(this.imageList);
    }
  },

  methods: {
    /**转格式 */
    changeArr(str){
      if(str.url == undefined){
        return;
      }
      let obj = {
        src : str.url,
        index : 1,
        show:false,
        other:str,
      }
      let arr = [];
      arr.push(obj);
      this.images = arr;
    },
    /**转格式 */
    changeArrT(arr){
      if(arr.length == 0){
        this.images=[];
        return;
      }
      let new_arr = [];
      arr.forEach((item,index)=>{
        let obj = {
          other:item,
          src : item.url,
          index : index,
          show:false
        }
        new_arr.push(obj);
      })

      this.images = new_arr;
    },
    //划入
    mouseOver(index){
      this.images[index].show = true;
    },
    //划出
    mouseLeave(index){
      this.images[index].show = false;
    },
    /**
     * 删除
     * */
    deleteImg(item){
      this.$emit('deleteImg',item);
    },
  },


}

</script>
<style lang='scss' scoped>
.imgDiv{
  display: inline-block;
  position: relative;
}
.iconStyle{
  position: absolute;
  right: 10px;
  top: 0;
  width: 30px;
  height: 30px;
  font-size: 25px;
  text-align: center;
  background: rgba(0,0,0,.7);
  color: #fff;
}
</style>