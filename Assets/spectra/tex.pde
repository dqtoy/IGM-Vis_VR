ArrayList<float[]> amps;
String[] name;
String[] point;
int step = 18;

void setup() {
  amps = new ArrayList<float[]>();
  String[] lines = loadStrings("out.txt");
  int a = 0;
  name = new String[lines.length];
  for (String line : lines) {
    name[a] = line.split(" ")[0].trim();
    String[] point = subset(line.split(" "), 1);
    float[] nums = new float[point.length];
    for (int i=0; i<point.length; i++)
      nums[i] = float(point[i]) / 2.0 * 255;
    amps.add(nums);
    a++;
    //System.out.println(name);
  }
  size(1000, 64);
  background(255);
  surface.setResizable(true);
}

void draw() {
  float[] pts = amps.get(step);
  surface.setSize(pts.length, 64);
  width = pts.length;
  for (int x=0; x<pts.length; x++)
    for (int y=0; y<64; y++)
      set(x, y, color(255-(int)pts[x], 255-(int)pts[x], 255-(int)pts[x]));
  get(0,0,pts.length,64).save(name[step] + ".png");
  //saveFrame(name[step] + ".png");
  System.out.println(name[step]);
  step++;
  background(255);
  if(step>amps.size()-1)
    noLoop();
}
